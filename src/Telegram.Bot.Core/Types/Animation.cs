namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents an animation file to be displayed in the message containing a <see cref="Game"/>.
    /// </summary>
    public class Animation
    {
        /// <summary>
        /// Unique file identifier.
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// Video width as defined by sender
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Video height as defined by sender
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Duration of the video in seconds as defined by sender
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Animation thumbnail as defined by sender.
        /// </summary>
        public PhotoSize Thumb { get; set; }

        /// <summary>
        /// Original animation filename as defined by sender.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// MIME type of the file as defined by sender.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// File size.
        /// </summary>
        public int FileSize { get; set; }
    }
}
