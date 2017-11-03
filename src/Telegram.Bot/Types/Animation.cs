using Newtonsoft.Json;

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
        [JsonProperty(Required = Required.Always)]
        public string FileId { get; set; }

        /// <summary>
        /// Animation thumbnail as defined by sender.
        /// </summary>
        [JsonProperty]
        public PhotoSize Thumb { get; set; }

        /// <summary>
        /// Original animation filename as defined by sender.
        /// </summary>
        [JsonProperty]
        public string FileName { get; set; }

        /// <summary>
        /// MIME type of the file as defined by sender.
        /// </summary>
        [JsonProperty]
        public string MimeType { get; set; }

        /// <summary>
        /// File size.
        /// </summary>
        [JsonProperty]
        public int FileSize { get; set; }
    }
}
