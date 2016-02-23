using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents link to a page containing an embedded video player or a video file.
    /// </summary>
    public class InlineQueryResultVideo : InlineQueryResult
    {
        /// <summary>
        /// Title of the result
        /// </summary>
        [JsonProperty("title", Required = Required.Always)]
        public new string Title { get; set; }

        /// <summary>
        /// Text of a message to be sent
        /// </summary>
        [JsonProperty("message_text", Required = Required.Always)]
        public new string MessageText { get; set; }
        
        /// <summary>
        /// A valid URL for the embedded video player or video file
        /// </summary>
        [JsonProperty("video_url", Required = Required.Always)]
        public string Url { get; set; }

        /// <summary>
        /// Mime type of the content of video url, i.e. "text/html" or "video/mp4"
        /// </summary>
        [JsonProperty("mime_type", Required = Required.Always)]
        public string MimeType { get; set; }

        /// <summary>
        /// Optional. Video width
        /// </summary>
        [JsonProperty("video_width", Required = Required.Default)]
        public int Width { get; set; }

        /// <summary>
        /// Optional. Video height
        /// </summary>
        [JsonProperty("video_height", Required = Required.Default)]
        public int Height { get; set; }

        /// <summary>
        /// Optional. Video duration in seconds
        /// </summary>
        [JsonProperty("video_duration", Required = Required.Default)]
        public int Duration { get; set; }

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty("description", Required = Required.Default)]
        public string Description { get; set; }
    }
}
