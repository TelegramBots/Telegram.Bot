#if NETSTANDARD2_1
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace Telegram.Bot.Extensions.Polling
{
    /// <summary>
    /// Exposes an IAsyncEnumerable of <see cref="Update"/>s that supports asynchronous iteration over <see cref="Update"/>s as they are received
    /// </summary>
    public interface IYieldingUpdateReceiver
    {
        /// <summary>
        /// Indicates how many <see cref="Update"/>s are ready to be returned by <see cref="YieldUpdatesAsync"/>
        /// </summary>
        int PendingUpdates { get; }

        /// <summary>
        /// Gets an IAsyncEnumerable of <see cref="Update"/>s that supports asynchronous iteration over <see cref="Update"/>s as they are received
        /// </summary>
        /// <returns>An <see cref="IAsyncEnumerable{T}"/> of <see cref="Update"/></returns>
        IAsyncEnumerable<Update> YieldUpdatesAsync();
    }
}
#endif
