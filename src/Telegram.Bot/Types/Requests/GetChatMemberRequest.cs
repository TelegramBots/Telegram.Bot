using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to get information about a chat member
    /// </summary>
    public class GetChatMemberRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetChatMemberRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="userId">Unique identifier of the target user</param>
        public GetChatMemberRequest(ChatId chatId, int userId) : base("getChatMember",
            new Dictionary<string, object>
            {
                {"chat_id", chatId},
                {"user_id", userId}
            })
        {

        }
    }
}
