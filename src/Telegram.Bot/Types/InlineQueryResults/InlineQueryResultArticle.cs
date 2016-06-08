using Newtonsoft.Json;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to an article or web page.
    /// </summary>
    public class InlineQueryResultArticle : InlineQueryResultNew
    {
        /// <summary>
        /// Optional. URL of the result
        /// </summary>
        [JsonProperty("url", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Url { get; set; }

        /// <summary>
        /// Optional. Pass <c>true</c>, if you don't want the URL to be shown in the message
        /// </summary>
        [JsonProperty("hide_url", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool HideUrl { get; set; } = false;

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty("description", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Description { get; set; }
    }
}
