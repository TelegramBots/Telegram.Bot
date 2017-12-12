using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Telegram.Bot.Helpers;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Send .webp stickers. On success, the sent <see cref="Message"/> is returned.
    /// </summary>
    public class SendStickerRequest : FileRequestBase<Message>,
                                      INotifiableMessage,
                                      IReplyMessage
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// Sticker to send
        /// </summary>
        public FileToSend Sticker { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ReplyToMessageId { get; set; }

        /// <summary>
        /// Additional interface options. A JSON-serialized object for an inline keyboard, custom reply keyboard, instructions to remove reply keyboard or to force a reply from the user.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public SendStickerRequest()
            : base("sendSticker")
        { }

        /// <summary>
        /// Initializes a new request chatId and sticker
        /// </summary>
        public SendStickerRequest(ChatId chatId, FileToSend sticker)
            : base("sendSticker")
        {
            ChatId = chatId;
            Sticker = sticker;
        }

        /// <summary>
        /// Generate content of HTTP message
        /// </summary>
        /// <param name="serializerSettings">JSON serialization setting</param>
        /// <returns>Content of HTTP request</returns>
        public override HttpContent ToHttpContent(JsonSerializerSettings serializerSettings)
        {
            HttpContent content;

            if (Sticker.Type == FileType.Stream)
            {
                var parameters = new Dictionary<string, object>
                {
                    { nameof(ChatId).ToSnakeCased(), ChatId },
                    { nameof(Sticker).ToSnakeCased(), Sticker },
                    { nameof(ReplyMarkup).ToSnakeCased(), ReplyMarkup }
                };

                if (ReplyToMessageId != default)
                {
                    parameters.Add(nameof(ReplyToMessageId).ToSnakeCased(), ReplyToMessageId);
                }

                if (DisableNotification != default)
                {
                    parameters.Add(nameof(DisableNotification).ToSnakeCased(), DisableNotification);
                }

                content = GetMultipartContent(parameters, serializerSettings);
            }
            else
            {
                content = base.ToHttpContent(serializerSettings);
            }

            return content;
        }
    }
}
