using Newtonsoft.Json;
using System.ComponentModel;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a video animation (H.264/MPEG-4 AVC video without sound).
    /// By default, this animated MPEG-4 file will be sent by the user with optional caption.
    /// Alternatively, you can provide message_text to send it instead of the animation.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultMpeg4Gif : InlineQueryResultNew
    {
        /// <summary>
        /// A valid URL for the MP4 file. File size must not exceed 1MB
        /// </summary>
        [JsonProperty("mpeg4_url", Required = Required.Always)]
        public string Url { get; set; }

        /// <summary>
        /// Optional. Video width
        /// </summary>
        [JsonProperty("mpeg4_width", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Width { get; set; }

        /// <summary>
        /// Optional. Video height
        /// </summary>
        [JsonProperty("mpeg4_height", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Height { get; set; }

        /// <summary>
        /// Optional. Duration of the Video
        /// </summary>
        [JsonProperty("mpeg4_duration", Required = Required.Default)]
        public int Duration { get; set; }

        /// <summary>
        /// Optional. Caption of the MPEG-4 file to be sent
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Caption { get; set; }

        /// <summary>
        /// Optional. Thumbnail width
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new int ThumbWidth { get; set; }

        /// <summary>
        /// Optional. Thumbnail height
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new int ThumbHeight { get; set; }
    }
}
