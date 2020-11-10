using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Requests;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Copy messages of any kind
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class CopyMessageRequest : RequestBase<MessageId>,
                                      INotifiableMessage,
                                      IReplyMessage,
                                      IFormattableMessage,
                                      IReplyMarkupMessage<IReplyMarkup>

    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ChatId ChatId { get; }

        /// <summary>
        /// Unique identifier for the chat where the original message was sent (or channel username in the format @channelusername)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ChatId FromChatId { get; }

        /// <summary>
        /// Message identifier in the chat specified in <see cref="FromChatId"/>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int MessageId { get; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ReplyToMessageId { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool AllowSendingWithoutReply { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ParseMode ParseMode { get; set; }


        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<MessageEntity> CaptionEntities { get; set; }

        /// <summary>
        /// New caption for media, 0-1024 characters after entities parsing. If not specified, the original caption is kept
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }

        /// <summary>
        /// Initializes a new request with chatId, fromChatId and messageId
        /// </summary>
        public CopyMessageRequest(ChatId chatId, ChatId fromChatId, int messageId) : base("copyMessage")
        {
            ChatId = chatId;
            FromChatId = fromChatId;
            MessageId = messageId;
        }

    }
}
