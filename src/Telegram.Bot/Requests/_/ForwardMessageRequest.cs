using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Forward messages of any kind
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ForwardMessageRequest : RequestBase<Message>, INotifiableMessage
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

        /// <summary>
        /// Initializes a new request with chatId, fromChatId and messageId
        /// </summary>
        public ForwardMessageRequest(ChatId chatdId, ChatId fromChatId, int messageId)
            : base("forwardMessage")
        {
            ChatId = chatdId;
            FromChatId = fromChatId;
            MessageId = messageId;
        }
    }
}
