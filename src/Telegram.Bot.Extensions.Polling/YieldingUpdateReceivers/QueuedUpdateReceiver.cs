#if NETCOREAPP3_0
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Polling
{
    public class QueuedUpdateReceiver : IYieldingUpdateReceiver
    {
        public readonly ITelegramBotClient BotClient;

        public QueuedUpdateReceiver(ITelegramBotClient botClient)
        {
            BotClient = botClient;
        }

        public async IAsyncEnumerable<Update> YieldUpdatesAsync(
            UpdateType[]? allowedUpdates = default,
            Func<Exception, Task>? exceptionHandler = default,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
            yield return new Update();
        }
    }
}
#endif