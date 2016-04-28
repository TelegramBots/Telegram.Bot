using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a link to an animated GIF file.
    /// By default, this animated GIF file will be sent by the user with optional caption.
    /// Alternatively, you can provide message_text to send it instead of the animation.
    /// </summary>
    public class InlineQueryResultGif : InlineQueryResultNew
    {
        /// <summary>
        /// A valid URL for the GIF file. File size must not exceed 1MB
        /// </summary>
        [JsonProperty("gif_url", Required = Required.Always)]
        public string Url { get; set; }

        /// <summary>
        /// Optional. Width of the GIF
        /// </summary>
        [JsonProperty("gif_width", Required = Required.Default)]
        public int Width { get; set; }

        /// <summary>
        /// Optional. Height of the GIF
        /// </summary>
        [JsonProperty("gif_height", Required = Required.Default)]
        public int Height { get; set; }

        /// <summary>
        /// Optional. Caption of the GIF file to be sent
        /// </summary>
        [JsonProperty("caption", Required = Required.Default)]
        public string Caption { get; set; }

        [JsonIgnore]
        public new string ThumbUrl { get; set; }

        [JsonIgnore]
        public new string ThumbWidth { get; set; }

        [JsonIgnore]
        public new string ThumbHeight { get; set; }

    }
}
