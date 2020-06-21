using System;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Polling
{
    /// <summary>
    /// Options to configure getUpdates requests
    /// </summary>
    public sealed class ReceiveOptions
    {
        private int? _limit;

        /// <summary>
        /// Identifier of the first update to be returned.
        /// </summary>
        public int? Offset { get; set; }

        /// <summary>
        /// Indicates which <see cref="UpdateType"/>s are allowed to be received.
        /// In case of <c>null</c> the previous setting will be used
        /// </summary>
        public UpdateType[]? AllowedUpdates { get; set; }
        /// <summary>
        /// Limits the number of updates to be retrieved. Values between 1-100 are accepted.
        /// Defaults to 100 when is set to <c>null</c>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the value doesn't satisfies constraints
        /// </exception>
        public int? Limit
        {
            get => _limit;
            set
            {
                if (value < 1 || value > 100)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        $"'{nameof(Limit)}' can not be less than 1 or greater than 100");
                }
                _limit = value;
            }
        }
    }
}
