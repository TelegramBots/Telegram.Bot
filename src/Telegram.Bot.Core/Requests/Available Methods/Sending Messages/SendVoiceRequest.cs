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
    /// Send audio files, if you want Telegram clients to display the file as a playable voice message
    /// </summary>
    public class SendVoiceRequest : FileRequestBase<Message>,
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
        /// Audio file to send
        /// </summary>
        public InputOnlineFile Voice { get; }

        /// <summary>
        /// Duration of the voice message in seconds
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Voice message caption, 0-1024 characters
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
        /// Initializes a new request with chatId and voice
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel.</param>
        /// <param name="voice">Voice to send.</param>
        public SendVoiceRequest(ChatId chatId, InputOnlineFile voice, ITelegramBotJsonConverter jsonConverter)
            : base(jsonConverter, "sendVoice")
        {
            ChatId = chatId;
            Voice = voice;
        }

        /// <param name="ct"></param>
        /// <inheritdoc />
        public override async ValueTask<HttpContent> ToHttpContentAsync(CancellationToken ct) =>
            Voice.FileType == FileType.Stream
                ? await ToMultipartFormDataContentAsync("voice", Voice, ct)
                : await base.ToHttpContentAsync(ct);
    }
}
