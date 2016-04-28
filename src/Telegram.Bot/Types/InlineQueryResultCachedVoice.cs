using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a link to a voice message stored on the Telegram servers. By default, this voice message will be sent by the user. Alternatively, you can use input_message_content to send a message with the specified content instead of the voice message.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultCachedVoice : InlineQueryResultCached
    {
        /// <summary>
        /// A valid file identifier for the voice message
        /// </summary>
        [JsonProperty("voice_file_id", Required = Required.Always)]
        public string FileId { get; set; }

        [JsonIgnore]
        public new string Caption { get; set; }
    }
}
