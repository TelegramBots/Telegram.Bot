namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a video message (available in Telegram apps as of v.4.0).
    /// </summary>
    public class VideoNote : FileBase
    {
        /// <summary>
        /// Video width and height as defined by sender
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Duration of the video in seconds as defined by sender
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Optional. Video thumbnail
        /// </summary>
        public PhotoSize Thumb { get; set; }
    }
}
