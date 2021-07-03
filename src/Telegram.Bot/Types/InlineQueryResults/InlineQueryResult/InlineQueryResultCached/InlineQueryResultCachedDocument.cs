using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a file stored on the Telegram servers. By default, this file will be sent by the user with an optional caption. Alternatively, you can use <see cref="InlineQueryResultCachedDocument.InputMessageContent"/> to send a message with the specified content instead of the file.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultCachedDocument : InlineQueryResult,
                                                   ICaptionInlineQueryResult
    {
        /// <summary>
        /// Type of the result, must be document
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public override InlineQueryResultType Type => InlineQueryResultType.Document;

        /// <summary>
        /// Title for the result
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Title { get; }

        /// <summary>
        /// A valid file identifier for the file
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string DocumentFileId { get; }

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Description { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Caption { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ParseMode? ParseMode { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public MessageEntity[]? CaptionEntities { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InputMessageContent? InputMessageContent { get; set; }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="documentFileId">A valid file identifier for the file</param>
        /// <param name="title">Title of the result</param>
        public InlineQueryResultCachedDocument(string id, string documentFileId, string title)
            : base(id)
        {
            DocumentFileId = documentFileId;
            Title = title;
        }
    }
}
