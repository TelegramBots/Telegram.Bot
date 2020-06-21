using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Polling
{
    /// <summary>
    /// Options to configure getUpdates requests
    /// </summary>
    public sealed class ReceiveOptions
    {
        /// <summary>
        /// Identifier of the first update to be returned
        /// </summary>
        public int? Offset { get; set; }

        /// <summary>
        /// Indicates which <see cref="UpdateType"/>s are allowed to be received. <c>null</c> means to leave it as it
        /// was set in previous getUpdates call
        /// </summary>
        public UpdateType[]? AllowedUpdates { get; set; }
        /// <summary>
        /// Limits the number of updates to be retrieved. Values between 1-100 are accepted. Defaults to 100.
        /// </summary>
        public int? Limit { get; set; }
    }
}
