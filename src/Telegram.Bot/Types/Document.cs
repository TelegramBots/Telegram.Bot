using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a general file (as opposed to <see cref="PhotoSize"/> and <see cref="Audio"/> files).
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Document : File
    {
        /// <summary>
        /// Document thumbnail as defined by sender
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PhotoSize Thumb { get; set; }

        /// <summary>
        /// Optional. Original filename as defined by sender
        /// </summary>
        [JsonProperty]
        public string FileName { get; set; }

        /// <summary>
        /// Optional. MIME type of the file as defined by sender
        /// </summary>
        [JsonProperty]
        public string MimeType { get; set; }
    }
}
