using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Get current webhook status.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class GetWebhookInfoRequest : ParameterlessRequest<WebhookInfo>
    {
        /// <summary>
        /// Initializes a new request
        /// </summary>
        public GetWebhookInfoRequest()
            : base("getWebhookInfo")
        { }
    }
}
