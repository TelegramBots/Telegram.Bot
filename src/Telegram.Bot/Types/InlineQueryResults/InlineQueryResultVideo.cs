using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents link to a page containing an embedded video player or a video file.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn,
                NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultVideo : InlineQueryResult,
                                          ICaptionInlineQueryResult,
                                          IThumbnailUrlInlineQueryResult,
                                          ITitleInlineQueryResult
    {
        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        public InlineQueryResultVideo()
        {
            Type = InlineQueryResultType.Video;
        }

        /// <summary>
        /// A valid URL for the embedded video player or video file
        /// </summary>
        [JsonProperty("video_url", Required = Required.Always)]
        public string Url { get; set; }

        /// <summary>
        /// Mime type of the content of video url, i.e. "text/html" or "video/mp4"
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string MimeType { get; set; }

        /// <summary>
        /// Optional. Video width
        /// </summary>
        [JsonProperty("video_width", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Width { get; set; }

        /// <summary>
        /// Optional. Video height
        /// </summary>
        [JsonProperty("video_height", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Height { get; set; }

        /// <summary>
        /// Optional. Video duration in seconds
        /// </summary>
        [JsonProperty("video_duration", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Duration { get; set; }

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Description { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string ThumbUrl { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Title { get; set; }
    }
}
