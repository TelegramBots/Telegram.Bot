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
    /// Send rounded video messages
    /// </summary>
    public class SendVideoNoteRequest : FileRequestBase<Message>,
                                        INotifiableMessage,
                                        IReplyMessage,
                                        IReplyMarkupMessage<IReplyMarkup>,
                                        IThumbMediaMessage
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; }

        /// <summary>
        /// Video note to send
        /// </summary>
        public InputTelegramFile VideoNote { get; }

        /// <summary>
        /// Duration of sent video in seconds
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Video width and height
        /// </summary>
        public int Length { get; set; }

        /// <inheritdoc />
        public InputMedia Thumb { get; set; }

        /// <inheritdoc />
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
        public int ReplyToMessageId { get; set; }

        /// <inheritdoc />
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request with chatId and video note
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="videoNote">Video note to send</param>
        public SendVideoNoteRequest(ChatId chatId, InputTelegramFile videoNote)
            : base("sendVideoNote")
        {
            ChatId = chatId;
            VideoNote = videoNote;
        }

        /// <param name="ct"></param>
        /// <inheritdoc />
        public override async ValueTask<HttpContent> ToHttpContentAsync(CancellationToken ct)
        {
            HttpContent httpContent;
            if (VideoNote.FileType == FileType.Stream || Thumb?.FileType == FileType.Stream)
            {
                var multipartContent = await GenerateMultipartFormDataContent(ct, "video_note", "thumb");
                if (VideoNote.FileType == FileType.Stream)
                {
                    multipartContent.AddStreamContent(VideoNote.Content, "video_note", VideoNote.FileName);
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
