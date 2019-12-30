namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents an audio file to be treated as music by the Telegram clients.
    /// </summary>
    public class Audio : FileBase
    {
        /// <summary>
        /// Duration of the audio in seconds as defined by sender
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Optional. Performer of the audio as defined by sender or by audio tags
        /// </summary>
        public string Performer { get; set; }

        /// <summary>
        /// Optional. Title of the audio as defined by sender or by audio tags
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Optional. MIME type of the file as defined by sender
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Optional. Thumbnail of the album cover to which the music file belongs
        /// </summary>
        public PhotoSize Thumb { get; set; }
    }
}
