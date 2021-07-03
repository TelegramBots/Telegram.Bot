using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to an MP3 audio file. By default, this audio file will be sent by the user. Alternatively, you can use <see cref="InlineQueryResultAudio.InputMessageContent"/> to send a message with the specified content instead of the audio.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultAudio : InlineQueryResult,
                                          ICaptionInlineQueryResult
    {
        /// <summary>
        /// Type of the result, must be audio
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public override InlineQueryResultType Type => InlineQueryResultType.Audio;

        /// <summary>
        /// A valid URL for the audio file
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string AudioUrl { get; }

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Title { get; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Caption { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ParseMode? ParseMode { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public MessageEntity[]? CaptionEntities { get; set; }

        /// <summary>
        /// Optional. Performer
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Performer { get; set; }

        /// <summary>
        /// Optional. Audio duration in seconds
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? AudioDuration { get; set; }

        /// <summary>
        /// Optional. Content of the message to be sent instead of the audio
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InputMessageContent? InputMessageContent { get; set; }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="audioUrl">A valid URL for the audio file</param>
        /// <param name="title">Title of the result</param>
        public InlineQueryResultAudio(string id, string audioUrl, string title)
            : base(id)
        {
            AudioUrl = audioUrl;
            Title = title;
        }
    }
}
