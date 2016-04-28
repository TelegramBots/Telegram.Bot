using Newtonsoft.Json;

namespace Telegram.Bot.Types
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
        [JsonProperty("photo_width", Required = Required.Default)]
        public int Width { get; set; }

        /// <summary>
        /// Optional. Height of the photo
        /// </summary>
        [JsonProperty("photo_height", Required = Required.Default)]
        public int Height { get; set; }

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty("description", Required = Required.Default)]
        public string Description { get; set; }

        /// <summary>
        /// Optional. Caption of the photo to be sent
        /// </summary>
        [JsonProperty("caption", Required = Required.Default)]
        public string Caption { get; set; }

        [JsonIgnore]
        public new string ThumbWidth { get; set; }

        [JsonIgnore]
        public new string ThumbHeight { get; set; }
    }
}
