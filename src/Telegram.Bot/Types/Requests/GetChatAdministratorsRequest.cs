using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to get a chat's administrators
    /// </summary>
    public class GetChatAdministratorsRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetChatAdministratorsRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        public GetChatAdministratorsRequest(ChatId chatId) : base("getChatAdministrators", 
            new Dictionary<string, object> { { "chat_id", chatId } })
        {

        }
    }
}
