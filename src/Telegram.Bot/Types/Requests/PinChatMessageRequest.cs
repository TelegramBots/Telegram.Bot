using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to pin a message in a supergroup
    /// </summary>
    public class PinChatMessageRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PinChatMessageRequest"/> class
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
        /// <param name="messageId">Identifier of a message to pin</param>
        /// <param name="disableNotification">Pass True, if it is not necessary to send a notification to all group members about the new pinned message</param>
        public PinChatMessageRequest(ChatId chatId, int messageId, bool disableNotification = false) : base("pinChatMessage",
            new Dictionary<string, object>()
            {
                { "chat_id", chatId },
                { "message_id", messageId }
            })
        {
            if (disableNotification)
                Parameters.Add("disable_notification", true);
        }
    }
}
