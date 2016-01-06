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
        public string Width { get; internal set; }

        /// <summary>
        /// Video height as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "height", Required = Required.Always)]
        public string Height { get; internal set; }

        /// <summary>
        /// Duration of the video in seconds as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "duration", Required = Required.Always)]
        public int Duration { get; internal set; }

        /// <summary>
        /// Video thumbnail
        /// </summary>
        [JsonProperty(PropertyName = "thumb", Required = Required.Default)]
        public PhotoSize Thumb { get; internal set; }

        /// <summary>
        /// Optional. Mime type of a file as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "mime_type", Required = Required.Default)]
        public string MimeType { get; internal set; }
    }
}
