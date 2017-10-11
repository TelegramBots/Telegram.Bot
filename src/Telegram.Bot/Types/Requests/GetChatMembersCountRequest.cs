using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to get the count of chat members
    /// </summary>
    public class GetChatMembersCountRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetChatMembersCountRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        public GetChatMembersCountRequest(ChatId chatId) : base("getChatMembersCount",
            new Dictionary<string, object> { { "chat_id", chatId } })
        {

        }
    }
}
