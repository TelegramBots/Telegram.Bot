// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a video to be sent
    /// </summary>
    public class InputMediaVideo : InputMediaBase
    {
        /// <summary>
        /// Optional. Video width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Optional. Video height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Optional. Video duration
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Initializes a new video media to send
        /// </summary>
        public InputMediaVideo()
        {
            Type = "video";
        }
    }
}
