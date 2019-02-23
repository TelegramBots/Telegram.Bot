#if NETCOREAPP3_0
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Polling
{
    public interface IYieldingUpdateReceiver
    {
        IAsyncEnumerable<Update> YieldUpdatesAsync(
            UpdateType[]? allowedUpdates = default,
            Func<Exception, Task>? exceptionHandler = default,
            CancellationToken cancellationToken = default
        );
    }
}
#endif