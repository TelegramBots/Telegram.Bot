using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Send .webp stickers. On success, the sent <see cref="Message"/> is returned.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SendStickerRequest : FileRequestBase<Message>,
                                      INotifiableMessage,
                                      IReplyMessage,
                                      IReplyMarkupMessage<IReplyMarkup>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ChatId ChatId { get; }

        /// <summary>
        /// Sticker to send
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public InputOnlineFile Sticker { get; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? ReplyToMessageId { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? AllowSendingWithoutReply { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request chatId and sticker
        /// </summary>
        public SendStickerRequest(ChatId chatId, InputOnlineFile sticker)
            : base("sendSticker")
        {
            ChatId = chatId;
            Sticker = sticker;
        }

        /// <inheritdoc />
        public override HttpContent ToHttpContent() =>
            Sticker.FileType == FileType.Stream
                ? ToMultipartFormDataContent("sticker", Sticker)
                : base.ToHttpContent();
    }
}
