using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a file. By default, this file will be sent by the user with an optional caption. Alternatively, you can use <see cref="InlineQueryResultDocument.InputMessageContent"/> to send a message with the specified content instead of the file. Currently, only .PDF and .ZIP files can be sent using this method.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultDocument : InlineQueryResult,
                                             ICaptionInlineQueryResult,
                                             IThumbnailInlineQueryResult
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
        /// A valid URL for the file
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string DocumentUrl { get; }

        /// <summary>
        /// Mime type of the content of the file, either “application/pdf” or “application/zip”
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string MimeType { get; }

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Description { get; set; }

        /// <summary>
        /// Optional. Content of the message to be sent instead of the file
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InputMessageContent? InputMessageContent { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? ThumbUrl { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? ThumbWidth { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? ThumbHeight { get; set; }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="documentUrl">A valid URL for the file</param>
        /// <param name="title">Title of the result</param>
        /// <param name="mimeType">Mime type of the content of the file, either “application/pdf” or “application/zip”</param>
        public InlineQueryResultDocument(string id, string documentUrl, string title, string mimeType)
            : base(id)
        {
            DocumentUrl = documentUrl;
            Title = title;
            MimeType = mimeType;
        }
    }
}
