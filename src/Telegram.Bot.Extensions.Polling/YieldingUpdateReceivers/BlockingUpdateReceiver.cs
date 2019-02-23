#if NETCOREAPP3_0
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Polling
{
    public class BlockingUpdateReceiver : IYieldingUpdateReceiver
    {
        private static readonly Update[] EmptyUpdates = { };

        public readonly ITelegramBotClient BotClient;

        public BlockingUpdateReceiver(ITelegramBotClient botClient)
        {
            BotClient = botClient;
        }

        public async IAsyncEnumerable<Update> YieldUpdatesAsync(
            UpdateType[]? allowedUpdates = default,
            Func<Exception, Task>? exceptionHandler = default,
            CancellationToken cancellationToken = default)
        {
            int messageOffset = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                int timeout = (int)BotClient.Timeout.TotalSeconds;
                var updates = EmptyUpdates;
                try
                {
                    updates = await BotClient.GetUpdatesAsync(
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
                    yield return update;
                    messageOffset = update.Id + 1;
                }
            }
        }
    }
}
#endif