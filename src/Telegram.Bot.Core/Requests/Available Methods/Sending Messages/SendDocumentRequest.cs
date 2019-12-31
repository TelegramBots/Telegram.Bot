using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Helpers;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Send documents
    /// </summary>
    public class SendDocumentRequest : FileRequestBase<Message>,
                                       INotifiableMessage,
                                       IReplyMessage,
                                       IReplyMarkupMessage<IReplyMarkup>,
                                       IFormattableMessage,
                                       IThumbMediaMessage
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; }

        /// <summary>
        /// Document to send.
        /// </summary>
        public InputOnlineFile Document { get; }

        /// <summary>
        /// Photo caption (may also be used when resending photos by file_id), 0-1024 characters
        /// </summary>
        public string Caption { get; set; }

        /// <inheritdoc />
        public InputMedia Thumb { get; set; }

        /// <inheritdoc />
        public ParseMode ParseMode { get; set; }

        /// <inheritdoc />
        public int ReplyToMessageId { get; set; }

        /// <inheritdoc />
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request with chatId and document
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="document">Document to send</param>
        public SendDocumentRequest(ChatId chatId, InputOnlineFile document, ITelegramBotJsonConverter jsonConverter)
            : base("sendDocument")
        {
            ChatId = chatId;
            Document = document;
        }

        /// <param name="ct"></param>
        /// <inheritdoc />
        public override async ValueTask<HttpContent> ToHttpContentAsync(CancellationToken ct)
        {
            HttpContent httpContent;
            if (Document.FileType == FileType.Stream || Thumb?.FileType == FileType.Stream)
            {
                var multipartContent = await GenerateMultipartFormDataContent(ct, "document", "thumb");
                if (Document.FileType == FileType.Stream)
                {
                    multipartContent.AddStreamContent(Document.Content, "document", Document.FileName);
                }

                if (Thumb?.FileType == FileType.Stream)
                {
                    multipartContent.AddStreamContent(Thumb.Content, "thumb", Thumb.FileName);
                }

                httpContent = multipartContent;
            }
            else
            {
                httpContent = await base.ToHttpContentAsync(ct);
            }

            return httpContent;
        }
    }
}
