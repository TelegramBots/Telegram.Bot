using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents an audio file (voice note).
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Audio : File
    {
        /// <summary>
        /// Duration of the audio in seconds as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "duration", Required = Required.Always)]
        public int Duration;

        /// <summary>
        /// Optional. MIME type of the file as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "mime_type", Required = Required.Default)]
        public string MimeType;
    }
}
