using Newtonsoft.Json;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Base class for uncached <see cref="InlineQueryResult"/>
    /// </summary>
    /// <seealso cref="Telegram.Bot.Types.InlineQueryResults.InlineQueryResult" />
    public class InlineQueryResultNew : InlineQueryResult
    {
        /// <summary>
        /// Optional. Url of the thumbnail for the result
        /// </summary>
        [JsonProperty("thumb_url", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string ThumbUrl { get; set; }

        /// <summary>
        /// Optional. Thumbnail width
        /// </summary>
        [JsonProperty("thumb_width", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int ThumbWidth { get; set; }

        /// <summary>
        /// Optional. Thumbnail height
        /// </summary>
        [JsonProperty("thumb_height", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int ThumbHeight { get; set; }
    }
}
