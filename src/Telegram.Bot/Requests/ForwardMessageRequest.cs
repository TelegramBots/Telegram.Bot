using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Forward messages of any kind
    /// </summary>
    public class ForwardMessageRequest : RequestBase<Message>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// Unique identifier for the chat where the original message was sent (or channel username in the format @channelusername)
        /// </summary>
        public ChatId FromChatId { get; set; }

        /// <summary>
        /// Sends the message silently. Users will receive a notification with no sound.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool DisableNotification { get; set; }

        /// <summary>
        /// Message identifier in the chat specified in <see cref="FromChatId"/>
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public ForwardMessageRequest()
            : base("forwardMessage")
        {
        }

        /// <summary>
        /// Initializes a new request with chatId, fromChatId and messageId
        /// </summary>
        public ForwardMessageRequest(ChatId chatdId, ChatId fromChatId, int messageId)
            : this()
        {
            ChatId = chatdId;
            FromChatId = fromChatId;
            MessageId = messageId;
        }
    }
}
