using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot
{
    public static class TelegramBotClientPollingExtensions
    {
        public static void StartReceiving(
            this ITelegramBotClient botClient,
            Func<Update, Task> updateHandler,
            Func<Exception, Task> exceptionHandler, // ApiRequestException, GeneralException, AS WELL AS exceptions from updateHandler - in that case stop receiving
            UpdateType[]? allowedUpdates = null,
            CancellationToken cancellationToken = default)
        {
            if (botClient == null)
                throw new ArgumentNullException(nameof(botClient));

            if (updateHandler == null)
                throw new ArgumentNullException(nameof(updateHandler));

            if (exceptionHandler == null)
                throw new ArgumentNullException(nameof(exceptionHandler));

            Task.Run(async () =>
            {
                try
                {
                    await ReceiveAsync(botClient, updateHandler, exceptionHandler, allowedUpdates, cancellationToken);
                }
                catch (Exception ex)
                {
                    await exceptionHandler(ex);
                }
            });
        }

        public static async Task ReceiveAsync(
            this ITelegramBotClient botClient,
            Func<Update, Task> updateHandler,
            Func<Exception, Task>? exceptionHandler = null, // ApiRequestException, GeneralException
            UpdateType[]? allowedUpdates = null,
            CancellationToken cancellationToken = default)
        {
            if (botClient == null)
                throw new ArgumentNullException(nameof(botClient));

            if (updateHandler == null)
                throw new ArgumentNullException(nameof(updateHandler));

            int messageOffset = 0;
            var emptyUpdates = new Update[] { };

            while (!cancellationToken.IsCancellationRequested)
            {
                int timeout = (int)botClient.Timeout.TotalSeconds;
                var updates = emptyUpdates;
                try
                {
                    updates = await botClient.GetUpdatesAsync(
                        messageOffset,
                        timeout: timeout,
                        allowedUpdates: allowedUpdates,
                        cancellationToken: cancellationToken
                    ).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    // Ignore
                }
                catch (Exception ex)
                {
                    if (exceptionHandler != null)
                    {
                        await exceptionHandler(ex).ConfigureAwait(false);
                    }
                }

                foreach (var update in updates)
                {
                    await updateHandler(update).ConfigureAwait(false);
                    messageOffset = update.Id + 1;
                }
            }
        }
    }
}
