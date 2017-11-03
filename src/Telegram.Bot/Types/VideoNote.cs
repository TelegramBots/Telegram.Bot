using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a video message (available in Telegram apps as of v.4.0).
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class VideoNote : File
    {
        /// <summary>
        /// Video width and height as defined by sender
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int Length { get; set; }

        /// <summary>
        /// Video width as defined by sender
        /// </summary>
        public int Width => Length;

        /// <summary>
        /// Video height as defined by sender
        /// </summary>
        public int Height => Length;

        /// <summary>
        /// Duration of the video in seconds as defined by sender
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int Duration { get; set; }

        /// <summary>
        /// Optional. Video thumbnail
        /// </summary>
        [JsonProperty]
        public PhotoSize Thumb { get; set; }
    }
}
