using System.ComponentModel;

using Newtonsoft.Json;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a photo.
    /// By default, this photo will be sent by the user with optional caption.
    /// Alternatively, you can provide message_text to send it instead of photo.
    /// </summary>
    public class InlineQueryResultPhoto : InlineQueryResultNew
    {
        /// <summary>
        /// A valid URL of the photo. Photo size must not exceed 5MB
        /// </summary>
        [JsonProperty("photo_url", Required = Required.Always)]
        public string Url { get; set; }

        /// <summary>
        /// Optional. Width of the photo
        /// </summary>
        [JsonProperty("photo_width", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Width { get; set; }

        /// <summary>
        /// Optional. Height of the photo
        /// </summary>
        [JsonProperty("photo_height", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Height { get; set; }

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty("description", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Description { get; set; }

        /// <summary>
        /// Optional. Caption of the photo to be sent
        /// </summary>
        [JsonProperty("caption", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Caption { get; set; }

        /// <summary>
        /// Optional. Thumbnail width
        /// </summary>
        [JsonIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new int ThumbWidth { get; set; }

        /// <summary>
        /// Optional. Thumbnail height
        /// </summary>
        [JsonIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new int ThumbHeight { get; set; }
    }
}
