using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to unban a chat member
    /// </summary>
    public class UnbanChatMemberRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnbanChatMemberRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target group</param>
        /// <param name="userId">Unique identifier of the target user</param>
        public UnbanChatMemberRequest(ChatId chatId, int userId) : base("unbanChatMember",
            new Dictionary<string, object>
            {
                {"chat_id", chatId},
                {"user_id", userId}
            })
        {

        }
    }
}
