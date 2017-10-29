using Newtonsoft.Json;
using System.ComponentModel;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to an animated GIF file.
    /// By default, this animated GIF file will be sent by the user with optional caption.
    /// Alternatively, you can provide message_text to send it instead of the animation.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
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
        [JsonProperty("gif_width")]
        public int Width { get; set; }

        /// <summary>
        /// Optional. Height of the GIF
        /// </summary>
        [JsonProperty("gif_height", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Height { get; set; }

        /// <summary>
        /// Optional. Duration of the GIF
        /// </summary>
        [JsonProperty("gif_duration")]
        public int Duration { get; set; }

        /// <summary>
        /// Optional. Caption of the GIF file to be sent
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Caption { get; set; }

        /// <summary>
        /// Optional. Url of the thumbnail for the result
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string ThumbUrl { get; set; }

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
