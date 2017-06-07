using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a video message (available in Telegram apps as of v.4.0).
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class VideoNote : File
    {
        // BUG: Don't think this is right in the documentation
        /// <summary>
        /// Video width and height as defined by sender
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Duration of the video in seconds as defined by sender
        /// </summary>
        [JsonProperty("duration", Required = Required.Always)]
        public int Duration { get; set; }

        /// <summary>
        /// Optional. Video thumbnail
        /// </summary>
        [JsonProperty("thumb")]
        public PhotoSize Thumb { get; set; }
    }
}
