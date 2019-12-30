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
    /// Send audio files, if you want Telegram clients to display them in the music player. Your audio must be in the .mp3 format.
    /// </summary>
    public class SendAudioRequest : FileRequestBase<Message>,
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
        /// Audio file to send
        /// </summary>
        public InputOnlineFile Audio { get; }

        /// <summary>
        /// Photo caption (may also be used when resending photos by file_id), 0-1024 characters
        /// </summary>
        public string Caption { get; set; }

        /// <inheritdoc />
        public ParseMode ParseMode { get; set; }

        /// <summary>
        /// Duration of the audio in seconds
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Performer
        /// </summary>
        public string Performer { get; set; }

        /// <summary>
        /// Track name
        /// </summary>
        public string Title { get; set; }

        /// <inheritdoc />
        public InputMedia Thumb { get; set; }

        /// <inheritdoc />
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
        public int ReplyToMessageId { get; set; }

        /// <inheritdoc />
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request with chatId and audio
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="audio">Audio to send</param>
        public SendAudioRequest(ChatId chatId, InputOnlineFile audio, ITelegramBotJsonConverter jsonConverter)
            : base(jsonConverter, "sendAudio")
        {
            ChatId = chatId;
            Audio = audio;
        }

        /// <param name="ct"></param>
        /// <inheritdoc />
        public override async ValueTask<HttpContent> ToHttpContentAsync(CancellationToken ct)
        {
            HttpContent httpContent;
            if (Audio.FileType == FileType.Stream || Thumb?.FileType == FileType.Stream)
            {
                var multipartContent = await GenerateMultipartFormDataContent(ct, "audio", "thumb");
                if (Audio.FileType == FileType.Stream)
                {
                    multipartContent.AddStreamContent(Audio.Content, "audio", Audio.FileName);
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
