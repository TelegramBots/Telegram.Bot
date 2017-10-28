using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents an audio file to be treated as music by the Telegram clients.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Audio : File
    {
        /// <summary>
        /// Duration of the audio in seconds as defined by sender
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int Duration { get; set; }

        /// <summary>
        /// Performer of the audio as defined by sender or by audio tags
        /// </summary>
        [JsonProperty]
        public string Performer { get; set; }

        /// <summary>
        /// Title of the audio as defined by sender or by audio tags
        /// </summary>
        [JsonProperty]
        public string Title { get; set; }

        /// <summary>
        /// Optional. MIME type of the file as defined by sender
        /// </summary>
        [JsonProperty]
        public string MimeType { get; set; }
    }
}
