using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to get a chat
    /// </summary>
    public class GetChatRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetChatRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        public GetChatRequest(ChatId chatId) : base("getChat", new Dictionary<string, object> { { "chat_id", chatId } })
        {

        }
    }
}
