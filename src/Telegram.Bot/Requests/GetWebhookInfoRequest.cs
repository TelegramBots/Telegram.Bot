using Telegram.Bot.Types;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Get current webhook status.
    /// </summary>
    public class GetWebhookInfoRequest : RequestBase<WebhookInfo>
    {
        /// <summary>
        /// Initializes a new request
        /// </summary>
        public GetWebhookInfoRequest()
            : base("getWebhookInfo")
        { }
    }
}
