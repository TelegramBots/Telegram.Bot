using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a link to a video animation (H.264/MPEG-4 AVC video without sound).
    /// By default, this animated MPEG-4 file will be sent by the user with optional caption.
    /// Alternatively, you can provide message_text to send it instead of the animation.
    /// </summary>
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
        [JsonProperty("mpeg4_width", Required = Required.Default)]
        public int Width { get; set; }

        /// <summary>
        /// Optional. Video height
        /// </summary>
        [JsonProperty("mpeg4_height", Required = Required.Default)]
        public int Height { get; set; }

        /// <summary>
        /// Optional. Caption of the MPEG-4 file to be sent
        /// </summary>
        [JsonProperty("caption", Required = Required.Default)]
        public string Caption { get; set; }

        [JsonIgnore]
        public new string ThumbWidth { get; set; }

        [JsonIgnore]
        public new string ThumbHeight { get; set; }
    }
}
