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
        [JsonProperty(Required = Required.Always)]
        public int Width { get; set; }

        /// <summary>
        /// Video height as defined by sender
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int Height { get; set; }

        /// <summary>
        /// Duration of the video in seconds as defined by sender
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int Duration { get; set; }

        /// <summary>
        /// Video thumbnail
        /// </summary>
        [JsonProperty]
        public PhotoSize Thumb { get; set; }

        /// <summary>
        /// Optional. Mime type of a file as defined by sender
        /// </summary>
        [JsonProperty]
        public string MimeType { get; set; }
    }
}
