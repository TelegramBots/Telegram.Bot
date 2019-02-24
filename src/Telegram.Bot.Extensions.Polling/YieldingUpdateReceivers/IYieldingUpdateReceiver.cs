#if NETCOREAPP3_0
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace Telegram.Bot.Extensions.Polling
{
    public interface IYieldingUpdateReceiver
    {
        int PendingUpdates { get; }

        int MessageOffset { get; }

        IAsyncEnumerable<Update> YieldUpdatesAsync();
    }
}
#endif