using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a link to an mp3 audio file. By default, this audio file will be sent by the user. Alternatively, you can use input_message_content to send a message with the specified content instead of the audio.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultAudio : InlineQueryResult
    {
        /// <summary>
        /// Title of the result
        /// </summary>
        [JsonProperty("title", Required = Required.Always)]
        public new string Title { get; set; }

        /// <summary>
        /// A valid URL for the audio file
        /// </summary>
        [JsonProperty("audio_url", Required = Required.Always)]
        public string Url { get; set; }

        /// <summary>
        /// Optional. Performer
        /// </summary>
        [JsonProperty("performer", Required = Required.Default)]
        public string Performer { get; set; }

        /// <summary>
        /// Optional. Audio duration in seconds
        /// </summary>
        [JsonProperty("audio_duration", Required = Required.Always)]
        public int Duration { get; set; }

        /// <summary>
        /// Optional. Content of the message to be sent instead of the audio
        /// </summary>
        [JsonProperty("input_message_content", Required = Required.Default)]
        public InputMessageContent InputMessageContent { get; set; }

        [JsonIgnore]
        public new string MessageText { get; set; }

        [JsonIgnore]
        public new ParseMode ParseMode { get; set; }

        [JsonIgnore]
        public new string ThumbUrl { get; set; }

        [JsonIgnore]
        public new bool DisableWebPagePreview { get; set; }

    }
}
