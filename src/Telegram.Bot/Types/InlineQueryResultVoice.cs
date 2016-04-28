using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a link to a voice recording in an .ogg container encoded with OPUS. By default, this voice recording will be sent by the user. Alternatively, you can use input_message_content to send a message with the specified content instead of the the voice message.
    /// </summary>
    /// <remarks>
    /// This will only work in Telegram versions released after 9 April, 2016. Older clients will ignore them.
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultVoice : InlineQueryResultNew
    {
        /// <summary>
        /// A valid URL for the voice recording
        /// </summary>
        [JsonProperty("voice_url", Required = Required.Always)]
        public string Url { get; set; }

        /// <summary>
        /// Optional. Recording duration in seconds
        /// </summary>
        [JsonProperty("voice_duration", Required = Required.Always)]
        public int Duration { get; set; }

        [JsonIgnore]
        public new string ThumbUrl { get; set; }

        [JsonIgnore]
        public new string ThumbWidth { get; set; }

        [JsonIgnore]
        public new string ThumbHeight { get; set; }
    }
}
