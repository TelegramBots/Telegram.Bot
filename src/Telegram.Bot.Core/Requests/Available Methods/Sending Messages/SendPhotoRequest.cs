using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Send photos
    /// </summary>
    public class SendPhotoRequest : FileRequestBase<Message>,
                                    INotifiableMessage,
                                    IReplyMessage,
                                    IReplyMarkupMessage<IReplyMarkup>,
                                    IFormattableMessage
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; }

        /// <summary>
        /// Photo to send
        /// </summary>
        public InputOnlineFile Photo { get; }

        /// <summary>
        /// Photo caption (may also be used when resending photos by file_id), 0-1024 characters
        /// </summary>
        public string Caption { get; set; }

        /// <inheritdoc />
        public ParseMode ParseMode { get; set; }

        /// <inheritdoc />
        public int ReplyToMessageId { get; set; }

        /// <inheritdoc />
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request with chatId and photo
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="photo">Photo to send</param>
        public SendPhotoRequest(ChatId chatId, InputOnlineFile photo, ITelegramBotJsonConverter jsonConverter)
            : base(jsonConverter, "sendPhoto")
        {
            ChatId = chatId;
            Photo = photo;
        }

        /// <param name="ct"></param>
        /// <inheritdoc />
        public override async ValueTask<HttpContent> ToHttpContentAsync(CancellationToken ct) =>
            Photo.FileType == FileType.Stream
                ? await ToMultipartFormDataContentAsync("photo", Photo, ct)
                : await base.ToHttpContentAsync(ct);
    }
}
