namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a general file (as opposed to <see cref="PhotoSize"/> and <see cref="Audio"/> files).
    /// </summary>
    public class Document : FileBase
    {
        /// <summary>
        /// Document thumbnail as defined by sender
        /// </summary>
        public PhotoSize Thumb { get; set; }

        /// <summary>
        /// Optional. Original filename as defined by sender
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Optional. MIME type of the file as defined by sender
        /// </summary>
        public string MimeType { get; set; }
    }
}
