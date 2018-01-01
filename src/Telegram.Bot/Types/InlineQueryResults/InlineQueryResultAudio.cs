using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;
using Telegram.Bot.Types.InputMessageContents;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to an mp3 audio file stored on the Telegram servers. By default, this audio file will be sent by the user. Alternatively, you can use input_message_content to send a message with the specified content instead of the audio.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn,
                NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultAudio : InlineQueryResult,
                                          ICaptionInlineQueryResult,
                                          ITitleInlineQueryResult,
                                          IInputMessageContentResult
    {
        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        public InlineQueryResultAudio()
        {
            Type = InlineQueryResultType.Audio;
        }

        /// <summary>
        /// A valid file identifier for the audio file
        /// </summary>
        [JsonProperty("audio_file_id", Required = Required.Always)]
        public string FileId { get; set; }

        /// <summary>
        /// A valid URL for the audio file
        /// </summary>
        [JsonProperty("audio_url", Required = Required.Always)]
        public string Url { get; set; }

        /// <summary>
        /// Optional. Performer
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Performer { get; set; }

        /// <summary>
        /// Optional. Audio duration in seconds
        /// </summary>
        [JsonProperty("audio_duration", Required = Required.Always, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Duration { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Title { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public InputMessageContent InputMessageContent { get; set; }
    }
}
