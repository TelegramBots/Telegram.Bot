using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to an MP3 audio file stored on the Telegram servers. By default, this audio file will be sent by the user. Alternatively, you can use <see cref="InlineQueryResultCachedAudio.InputMessageContent"/> to send a message with the specified content instead of the audio.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultCachedAudio : InlineQueryResult,
                                                ICaptionInlineQueryResult
    {
        /// <summary>
        /// Type of the result, must be audio
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public override InlineQueryResultType Type => InlineQueryResultType.Audio;

        /// <summary>
        /// A valid file identifier for the audio file
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string AudioFileId { get; }

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
        /// Optional. Content of the message to be sent instead of the audio
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InputMessageContent? InputMessageContent { get; set; }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="audioFileId">A valid file identifier for the audio file</param>
        public InlineQueryResultCachedAudio(string id, string audioFileId)
            : base(id)
        {
            AudioFileId = audioFileId;
        }
    }
}
