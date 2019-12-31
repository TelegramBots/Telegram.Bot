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
    /// Send animation files (GIF or H.264/MPEG-4 AVC video without sound). Bots can currently send animation files of
    /// up to 50 MB in size, this limit may be changed in the future.
    /// </summary>
    public class SendAnimationRequest : FileRequestBase<Message>,
                                        IChatMessage,
                                        INotifiableMessage,
                                        IReplyMessage,
                                        IReplyMarkupMessage<IReplyMarkup>,
                                        IFormattableMessage,
                                        IThumbMediaMessage
    {
        /// <inheritdoc />
        public ChatId ChatId { get; }

        /// <summary>
        /// Animation to send
        /// </summary>
        public InputOnlineFile Animation { get; }

        /// <summary>
        /// Duration of the video in seconds
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Video width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Video height
        /// </summary>
        public int Height { get; set; }

        /// <inheritdoc />
        public InputMedia Thumb { get; set; }

        /// <summary>
        /// Video caption (may also be used when resending videos by file_id), 0-1024 characters
        /// </summary>
        public string Caption { get; set; }

        /// <inheritdoc />
        public ParseMode ParseMode { get; set; }

        /// <inheritdoc />
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
        public int ReplyToMessageId { get; set; }

        /// <inheritdoc />
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request with chatId and animation
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="animation">Animation to send</param>
        public SendAnimationRequest(ChatId chatId, InputOnlineFile animation, ITelegramBotJsonConverter jsonConverter)
            : base("sendAnimation")
        {
            ChatId = chatId;
            Animation = animation;
        }

        /// <param name="ct"></param>
        /// <inheritdoc />
        public override async ValueTask<HttpContent> ToHttpContentAsync(CancellationToken ct)
        {
            HttpContent httpContent;
            if (Animation.FileType == FileType.Stream || Thumb?.FileType == FileType.Stream)
            {
                var multipartContent = await GenerateMultipartFormDataContent(ct, "animation", "thumb");
                if (Animation.FileType == FileType.Stream)
                {
                    multipartContent.AddStreamContent(Animation.Content, "animation", Animation.FileName);
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
