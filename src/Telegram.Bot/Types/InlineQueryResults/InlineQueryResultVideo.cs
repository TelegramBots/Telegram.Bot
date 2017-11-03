using Newtonsoft.Json;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents link to a page containing an embedded video player or a video file.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultVideo : InlineQueryResultNew
    {
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

        /// <summary>
        /// Optional. Caption of the photo to be sent
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Caption { get; set; }
    }
}
