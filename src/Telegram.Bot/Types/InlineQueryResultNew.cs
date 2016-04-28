using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    public class InlineQueryResultNew : InlineQueryResult
    {
        /// <summary>
        /// Optional. Url of the thumbnail for the result
        /// </summary>
        [JsonProperty("thumb_url", Required = Required.Default)]
        public string ThumbUrl { get; set; }

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
