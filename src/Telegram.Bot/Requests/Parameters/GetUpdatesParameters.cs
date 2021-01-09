using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.GetUpdatesAsync" /> method.
    /// </summary>
    public class GetUpdatesParameters : ParametersBase
    {
        /// <summary>
        ///     Identifier of the first
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        ///     Limits the number of updates to be retrieved. Values between 1-100 are accepted.
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        ///     Timeout in seconds for long polling. Defaults to 0, i.e. usual short polling. Should be positive, short polling
        ///     should be used for testing purposes only.
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        ///     List the
        /// </summary>
        public IEnumerable<UpdateType> AllowedUpdates { get; set; }

        /// <summary>
        ///     Sets <see cref="Offset" /> property.
        /// </summary>
        /// <param name="offset">
        ///     Identifier of the first
        /// </param>
        public GetUpdatesParameters WithOffset(int offset)
        {
            Offset = offset;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Limit" /> property.
        /// </summary>
        /// <param name="limit">
        ///     Limits the number of updates to be retrieved. Values between 1-100 are accepted.
        /// </param>
        public GetUpdatesParameters WithLimit(int limit)
        {
            Limit = limit;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Timeout" /> property.
        /// </summary>
        /// <param name="timeout">
        ///     Timeout in seconds for long polling. Defaults to 0, i.e. usual short polling. Should be positive, short polling
        ///     should be used for testing purposes only.
        /// </param>
        public GetUpdatesParameters WithTimeout(int timeout)
        {
            Timeout = timeout;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="AllowedUpdates" /> property.
        /// </summary>
        /// <param name="allowedUpdates">
        ///     List the
        /// </param>
        public GetUpdatesParameters WithAllowedUpdates(IEnumerable<UpdateType> allowedUpdates)
        {
            AllowedUpdates = allowedUpdates;
            return this;
        }
    }
}