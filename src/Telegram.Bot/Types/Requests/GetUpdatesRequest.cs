using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to get updates
    /// </summary>
    public class GetUpdatesRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUpdatesRequest"/> class
        /// </summary>
        /// <param name="offset">
        /// Identifier of the first <see cref="Update"/> to be returned.
        /// Must be greater by one than the highest among the identifiers of previously received updates.
        /// By default, updates starting with the earliest unconfirmed update are returned. An update is considered
        /// confirmed as soon as GetUpdates is called with an offset higher than its <see cref="Update.Id"/>.
        /// The negative offset can be specified to retrieve updates starting from -offset update from the end of the updates queue. All previous updates will forgotten.
        /// </param>
        /// <param name="limit">
        /// Limits the number of updates to be retrieved. Values between 1—100 are accepted.
        /// </param>
        /// <param name="timeout">
        /// Timeout in seconds for long polling. Defaults to 0, i.e. usual short polling. Should be positive, short polling should be used for testing purposes only.
        /// </param>
        /// <param name="allowedUpdates">
        /// List the <see cref="UpdateType"/> of updates you want your bot to receive. See <see cref="UpdateType"/> for a complete list of available update types. Specify an empty list to receive all updates regardless of type (default).
        /// If not specified, the previous setting will be used.
        /// 
        /// Please note that this parameter doesn't affect updates created before the call to the GetUpdates method, so unwanted updates may be received for a short period of time.
        /// </param>
        public GetUpdatesRequest(int offset = 0, int limit = 100, int timeout = 0,
            UpdateType[] allowedUpdates = null) : base("getUpdates",
                new Dictionary<string, object>
                {
                    {"offset", offset},
                    {"limit", limit},
                    {"timeout", timeout}
                })
        {
            if (allowedUpdates != null && !allowedUpdates.Contains(UpdateType.All))
                Parameters.Add("allowed_updates", allowedUpdates);
        }
    }
}
