using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Forward messages of any kind
    /// </summary>
    public class ForwardMessageRequest : RequestBase<Message>, INotifiableMessage
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; }

        /// <summary>
        /// Unique identifier for the chat where the original message was sent (or channel username in the format @channelusername)
        /// </summary>
        public ChatId FromChatId { get; }

        /// <summary>
        /// Message identifier in the chat specified in <see cref="FromChatId"/>
        /// </summary>
        public int MessageId { get; }

        /// <inheritdoc />
        public bool DisableNotification { get; set; }

        /// <summary>
        /// Initializes a new request with chatId, fromChatId and messageId
        /// </summary>
        public ForwardMessageRequest(ChatId chatId, ChatId fromChatId, int messageId)
            : base("forwardMessage")
        {
            ChatId = chatId;
            FromChatId = fromChatId;
            MessageId = messageId;
        }
    }
}
