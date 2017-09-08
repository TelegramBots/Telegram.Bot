namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to get information about the current webhook
    /// </summary>
    public class GetWebhookInfoRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetWebhookInfoRequest"/> class
        /// </summary>
        public GetWebhookInfoRequest() : base("getWebhookInfo")
        {

        }
    }
}
