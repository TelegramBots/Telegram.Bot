using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a video file.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Video : File
    {
        /// <summary>
        /// Video width as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "width", Required = Required.Always)]
        public string Width;

        /// <summary>
        /// Video height as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "height", Required = Required.Always)]
        public string Height;

        /// <summary>
        /// Duration of the video in seconds as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "duration", Required = Required.Always)]
        public int Duration;

        /// <summary>
        /// Video thumbnail
        /// </summary>
        [JsonProperty(PropertyName = "thumb", Required = Required.Always)]
        public PhotoSize Thumb;

        /// <summary>
        /// Optional. Mime type of a file as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "mime_type", Required = Required.Default)]
        public string MimeType;

        /// <summary>
        /// Optional. Text description of the video (usually empty)
        /// </summary>
        [JsonProperty(PropertyName = "caption", Required = Required.Default)]
        public string Caption;
    }
}
