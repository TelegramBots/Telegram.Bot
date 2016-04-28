using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a link to an mp3 audio file stored on the Telegram servers. By default, this audio file will be sent by the user. Alternatively, you can use input_message_content to send a message with the specified content instead of the audio.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultCachedAudio : InlineQueryResultCached
    {
        /// <summary>
        /// A valid file identifier for the audio file
        /// </summary>
        [JsonProperty("audio_file_id", Required = Required.Always)]
        public string FileId { get; set; }

        [JsonIgnore]
        public new string Title { get; set; }

        [JsonIgnore]
        public new string Caption { get; set; }
    }
}
