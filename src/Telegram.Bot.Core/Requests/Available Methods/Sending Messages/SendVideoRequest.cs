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
    /// Send video files, Telegram clients support mp4 videos
    /// </summary>
    public class SendVideoRequest : FileRequestBase<Message>,
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
        /// Video file to send
        /// </summary>
        public InputOnlineFile Video { get; }

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

        /// <summary>
        /// Video caption (may also be used when resending videos by file_id), 0-1024 characters
        /// </summary>
        public string Caption { get; set; }

        /// <inheritdoc />
        public ParseMode ParseMode { get; set; }

        /// <summary>
        /// Pass True, if the uploaded video is suitable for streaming
        /// </summary>
        public bool SupportsStreaming { get; set; }

        /// <inheritdoc />
        public InputMedia Thumb { get; set; }

        /// <inheritdoc />
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
        public int ReplyToMessageId { get; set; }

        /// <inheritdoc />
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request with chatId and video
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="video">Video to send</param>
        public SendVideoRequest(ChatId chatId, InputOnlineFile video, ITelegramBotJsonConverter jsonConverter)
            : base(jsonConverter, "sendVideo")
        {
            ChatId = chatId;
            Video = video;
        }

        /// <param name="ct"></param>
        /// <inheritdoc />
        public override async ValueTask<HttpContent> ToHttpContentAsync(CancellationToken ct)
        {
            HttpContent httpContent;
            if (Video.FileType == FileType.Stream || Thumb?.FileType == FileType.Stream)
            {
                var multipartContent = await GenerateMultipartFormDataContent(ct, "video", "thumb");
                if (Video.FileType == FileType.Stream)
                {
                    multipartContent.AddStreamContent(Video.Content, "video", Video.FileName);
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
