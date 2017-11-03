using Newtonsoft.Json;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Base class for uncached <see cref="InlineQueryResult"/>
    /// </summary>
    /// <seealso cref="Telegram.Bot.Types.InlineQueryResults.InlineQueryResult" />
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultNew : InlineQueryResult
    {
        /// <summary>
        /// Optional. Url of the thumbnail for the result
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string ThumbUrl { get; set; }

        /// <summary>
        /// Optional. Thumbnail width
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int ThumbWidth { get; set; }

        /// <summary>
        /// Optional. Thumbnail height
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int ThumbHeight { get; set; }
    }
}
