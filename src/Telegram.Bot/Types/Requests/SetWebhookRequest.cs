using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to set a webhook
    /// </summary>
    public class SetWebhookRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetWebhookRequest"/> class
        /// </summary>
        /// <param name="url">HTTPS url to send updates to. Use an empty string to remove webhook integration</param>
        /// <param name="certificate">
        /// Upload your public key certificate so that the root certificate in use can be checked.
        /// See the <see href="https://core.telegram.org/bots/self-signed">self-signed guide</see> for details.
        /// </param>
        /// <param name="maxConnections">Maximum allowed number of simultaneous HTTPS connections to the webhook for update delivery, 1-100. Defaults to 40. Use lower values to limit the load on your bot‘s server, and higher values to increase your bot’s throughput.</param>
        /// <param name="allowedUpdates">
        /// List the <see cref="UpdateType"/> of updates you want your bot to receive. See <see cref="UpdateType"/> for a complete list of available update types. Specify an empty list to receive all updates regardless of type (default).
        /// If not specified, the previous setting will be used.
        /// 
        /// Please note that this parameter doesn't affect updates created before the call to the GetUpdates method, so unwanted updates may be received for a short period of time.
        /// </param>
        public SetWebhookRequest(string url = "", FileToSend? certificate = null,
            int maxConnections = 40,
            UpdateType[] allowedUpdates = null) : base("setWebhook",
                new Dictionary<string, object>
                {
                    {"url", url},
                    {"max_connections", maxConnections}
                })
        {
            if (allowedUpdates != null && !allowedUpdates.Contains(UpdateType.All))
                Parameters.Add("allowed_updates", allowedUpdates);

            if (certificate != null)
                Parameters.Add("certificate", certificate);
        }
    }
}
