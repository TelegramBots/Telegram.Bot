using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a video to be sent
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InputMediaVideo : InputMediaBase
    {
        /// <summary>
        /// Optional. Video width
        /// </summary>
        [JsonProperty]
        public int Width { get; set; }

        /// <summary>
        /// Optional. Video height
        /// </summary>
        [JsonProperty]
        public int Height { get; set; }

        /// <summary>
        /// Optional. Video duration
        /// </summary>
        [JsonProperty]
        public int Duration { get; set; }

        /// <summary>
        /// Optional. Pass True, if the uploaded video is suitable for streaming
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool SupportsStreaming { get; set; }

        /// <summary>
        /// Initializes a new video media to send
        /// </summary>
        public InputMediaVideo()
        {
            Type = "video";
        }
    }
}
