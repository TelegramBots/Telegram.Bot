using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot
{
    public static class TelegramBotClientPollingExtensions
    {
        public static void StartReceiving<TUpdateHandler>(this ITelegramBotClient botClient, CancellationToken cancellationToken = default)
            where TUpdateHandler : IUpdateHandler, new()
        {
            StartReceiving(botClient, new TUpdateHandler(), cancellationToken);
        }

        public static void StartReceiving(this ITelegramBotClient botClient, IUpdateHandler updateHandler, CancellationToken cancellationToken = default)
        {
            if (botClient == null)
                throw new ArgumentNullException(nameof(botClient));

            if (updateHandler == null)
                throw new ArgumentNullException(nameof(updateHandler));

            Task.Run(async () =>
            {
                try
                {
                    await ReceiveAsync(botClient, updateHandler, cancellationToken);
                }
                catch (Exception ex)
                {
                    await updateHandler.HandleError(ex);
                }
            });
        }

        public static Task ReceiveAsync<TUpdateHandler>(this ITelegramBotClient botClient, CancellationToken cancellationToken = default)
            where TUpdateHandler : IUpdateHandler, new()
        {
            return ReceiveAsync(botClient, new TUpdateHandler(), cancellationToken);
        }

        public static async Task ReceiveAsync(this ITelegramBotClient botClient, IUpdateHandler updateHandler, CancellationToken cancellationToken = default)
        {
            if (botClient == null)
                throw new ArgumentNullException(nameof(botClient));

            if (updateHandler == null)
                throw new ArgumentNullException(nameof(updateHandler));

            UpdateType[]? allowedUpdates = updateHandler.AllowedUpdates;
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
                    await updateHandler.HandleError(ex).ConfigureAwait(false);
                }

                foreach (var update in updates)
                {
                    await updateHandler.HandleUpdate(update).ConfigureAwait(false);
                    messageOffset = update.Id + 1;
                }
            }
        }
    }
}
