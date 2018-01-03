using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;
using Telegram.Bot.Types.InputMessageContents;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a voice message stored on the Telegram servers. By default, this voice message will be sent by the user. Alternatively, you can use input_message_content to send a message with the specified content instead of the voice message.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultCachedVoice : InlineQueryResult,
                                                ICaptionInlineQueryResult,
                                                ITitleInlineQueryResult,
                                                IInputMessageContentResult
    {
        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        public InlineQueryResultCachedVoice()
            : base(InlineQueryResultType.CachedVoice)
        { }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="fileId">A valid file identifier for the voice message</param>
        /// <param name="title">Title of the result</param>
        public InlineQueryResultCachedVoice(string id, string fileId, string title)
            : this()
        {
            Id = id;
            FileId = fileId;
            Title = title;
        }

        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public string Title { get; set; }

        /// <summary>
        /// A valid file identifier for the voice message
        /// </summary>
        [JsonProperty("voice_file_id", Required = Required.Always)]
        public string FileId { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InputMessageContent InputMessageContent { get; set; }
    }
}
