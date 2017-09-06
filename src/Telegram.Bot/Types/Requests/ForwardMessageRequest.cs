using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to forward a message
    /// </summary>
    public class ForwardMessageRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new object of the <see cref="ForwardMessageRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="fromChatId"><see cref="ChatId"/> for the chat where the original message was sent</param>
        /// <param name="messageId">Unique message identifier</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        public ForwardMessageRequest(ChatId chatId, ChatId fromChatId, int messageId,
            bool disableNotification = false) : base("forwardMessage",
                new Dictionary<string, object>
                {
                    { "chat_id", chatId},
                    {"from_chat_id", fromChatId},
                    {"message_id", messageId},
                })
        {
            if (disableNotification) Parameters.Add("disable_notification", true);
        }

    }
}
