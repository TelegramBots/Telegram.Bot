using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a link to a file. By default, this file will be sent by the user with an optional caption. Alternatively, you can use input_message_content to send a message with the specified content instead of the file. Currently, only .PDF and .ZIP files can be sent using this method.
    /// </summary>
    /// <remarks>
    /// This will only work in Telegram versions released after 9 April, 2016. Older clients will ignore them.
    /// </remarks>
    public class InlineQueryResultDocument : InlineQueryResultBase,
        ICaptionInlineQueryResult,
        IThumbnailInlineQueryResult,
        ITitleInlineQueryResult,
        IInputMessageContentResult
    {
        /// <summary>
        /// A valid URL for the file
        /// </summary>
        public string DocumentUrl { get; set; }

        /// <inheritdoc />
        public string Title { get; set; }

        /// <summary>
        /// Mime type of the content of the file, either “application/pdf” or “application/zip”
        /// </summary>
        public string MimeType { get; set; }

        /// <inheritdoc />
        public string Caption { get; set; }

        /// <inheritdoc />
        public ParseMode ParseMode { get; set; }

        /// <summary>
        /// Optional. Short description of the result
        /// </summary>
        public string Description { get; set; }

        /// <inheritdoc />
        public string ThumbUrl { get; set; }

        /// <inheritdoc />
        public int ThumbWidth { get; set; }

        /// <inheritdoc />
        public int ThumbHeight { get; set; }

        /// <inheritdoc />
        public InputMessageContentBase InputMessageContent { get; set; }

        private InlineQueryResultDocument()
            : base(InlineQueryResultType.Document)
        {
        }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="documentUrl">A valid URL for the file</param>
        /// <param name="title">Title of the result</param>
        /// <param name="mimeType">Mime type of the content of the file, either “application/pdf” or “application/zip”</param>
        public InlineQueryResultDocument(string id, string documentUrl, string title, string mimeType)
            : base(InlineQueryResultType.Document, id)
        {
            DocumentUrl = documentUrl;
            Title = title;
            MimeType = mimeType;
        }
    }
}
