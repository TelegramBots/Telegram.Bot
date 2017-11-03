using Newtonsoft.Json;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to an article or web page.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultArticle : InlineQueryResultNew
    {
        /// <summary>
        /// Optional. URL of the result
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Url { get; set; }

        /// <summary>
        /// Optional. Pass <c>true</c>, if you don't want the URL to be shown in the message
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool HideUrl { get; set; }

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Description { get; set; }
    }
}
