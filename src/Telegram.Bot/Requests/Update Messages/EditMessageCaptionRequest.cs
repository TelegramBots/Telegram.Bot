using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Edit captions and game messages sent by the bot. On success the edited <see cref="Message"/> is returned.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class EditMessageCaptionRequest : RequestBase<Message>,
                                             IInlineReplyMarkupMessage,
                                             IFormattableMessage
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ChatId ChatId { get; }

        /// <summary>
        /// Identifier of the sent message
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int MessageId { get; }

        /// <summary>
        /// New caption of the message
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ParseMode ParseMode { get; set; }

        /// <summary>
        /// List of special entities that appear in the caption, which can be specified instead of parse_mode
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<MessageEntity> CaptionEntities { get; set; }

        /// <inheritdoc cref="IInlineReplyMarkupMessage.ReplyMarkup" />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InlineKeyboardMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request with chatId, messageId and new caption
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="messageId">Identifier of the sent message</param>
        /// <param name="caption">New caption of the message</param>
        public EditMessageCaptionRequest(ChatId chatId, int messageId, string caption = default)
            : base("editMessageCaption")
        {
            ChatId = chatId;
            MessageId = messageId;
            Caption = caption;
        }
    }
}
