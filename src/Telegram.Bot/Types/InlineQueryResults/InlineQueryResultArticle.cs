using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to an article or web page.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultArticle : InlineQueryResultNew
    {
        /// <summary>
        /// Optional. URL of the result
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Url { get; set; }

        /// <summary>
        /// Optional. Pass <c>true</c>, if you don't want the URL to be shown in the message
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool HideUrl { get; set; }

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Description { get; set; }
    }
}
