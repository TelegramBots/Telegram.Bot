using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a link to an article or web page.
    /// </summary>
    public class InlineQueryResultArticle : InlineQueryResult
    {
        /// <summary>
        /// Optional. URL of the result
        /// </summary>
        [JsonProperty("url", Required = Required.Default)]
        public string Url { get; set; }

        /// <summary>
        /// Optional. Pass True, if you don't want the URL to be shown in the message
        /// </summary>
        [JsonProperty("hide_url", Required = Required.Default)]
        public bool HideUrl { get; set; } = false;

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty("description", Required = Required.Default)]
        public string Description { get; set; }

        /// <summary>
        /// Optional. Thumbnail width
        /// </summary>
        [JsonProperty("thumb_width", Required = Required.Default)]
        public string ThumbWidth { get; set; }

        /// <summary>
        /// Optional. Thumbnail height
        /// </summary>
        [JsonProperty("thumb_height", Required = Required.Default)]
        public string ThumbHeight { get; set; }
    }
}
