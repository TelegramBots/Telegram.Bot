using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a file stored on the Telegram servers. By default, this file will be sent by the user with an optional caption. Alternatively, you can use input_message_content to send a message with the specified content instead of the file. Currently, only pdf-files and zip archives can be sent using this method.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultCachedDocument : InlineQueryResultBase,
                                                   ICaptionInlineQueryResult,
                                                   ITitleInlineQueryResult,
                                                   IInputMessageContentResult
    {
        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public string Title { get; set; }

        /// <summary>
        /// A valid file identifier for the file
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string DocumentFileId { get; set; }

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Description { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InputMessageContent InputMessageContent { get; set; }
        
        private InlineQueryResultCachedDocument()
            : base(InlineQueryResultType.Document)
        { }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="documentFileId">A valid file identifier for the file</param>
        /// <param name="title">Title of the result</param>
        public InlineQueryResultCachedDocument(string id, string documentFileId, string title)
            : base(InlineQueryResultType.Document, id)
        {
            DocumentFileId = documentFileId;
            Title = title;
        }
    }
}
