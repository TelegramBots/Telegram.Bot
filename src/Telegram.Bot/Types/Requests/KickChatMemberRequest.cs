using System;
using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to kick a chat member
    /// </summary>
    public class KickChatMemberRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KickChatMemberRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target group</param>
        /// <param name="userId">Unique identifier of the target user</param>
        /// <param name="untilDate"><see cref="DateTime"/> when the user will be unbanned. If user is banned for more than 366 days or less than 30 seconds from the current time they are considered to be banned forever</param>
        public KickChatMemberRequest(ChatId chatId, int userId,
            DateTime untilDate = default(DateTime)) : base("kickChatMember",
                new Dictionary<string, object>
                {
                    {"chat_id", chatId},
                    {"user_id", userId},
                })
        {
            if (untilDate != default(DateTime)) Parameters.Add("until_date", untilDate);
        }
    }
}
