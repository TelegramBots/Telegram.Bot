using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to send a chat action
    /// </summary>
    public class SendChatActionRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendChatActionRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="chatAction">Type of action to broadcast. Choose one, depending on what the user is about to receive.</param>
        public SendChatActionRequest(ChatId chatId, ChatAction chatAction) 
            : base("sendChatAction", new Dictionary<string, object> { { "chat_id", chatId }, { "action", chatAction } })
        {

        }
    }
}
