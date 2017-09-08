using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to leave a chat
    /// </summary>
    public class LeaveChatRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveChatRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        public LeaveChatRequest(ChatId chatId) : base("leaveChat", new Dictionary<string, object> { { "chat_id", chatId } })
        {

        }
    }
}
