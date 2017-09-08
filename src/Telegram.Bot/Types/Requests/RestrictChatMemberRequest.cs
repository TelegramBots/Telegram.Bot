using System;
using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to restrict a chat member
    /// </summary>
    public class RestrictChatMemberRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestrictChatMemberRequest"/> class
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
        /// <param name="userId">Unique identifier of the target user</param>
        /// <param name="untilDate"><see cref="DateTime"/> when restrictions will be lifted for the user. If user is restricted for more than 366 days or less than 30 seconds from the current time, they are considered to be restricted forever</param>
        /// <param name="canSendMessages">Pass True, if the user can send text messages, contacts, locations and venues</param>
        /// <param name="canSendMediaMessages">Pass True, if the user can send audios, documents, photos, videos, video notes and voice notes, implies can_send_messages</param>
        /// <param name="canSendOtherMessages">Pass True, if the user can send animations, games, stickers and use inline bots, implies can_send_media_messages</param>
        /// <param name="canAddWebPagePreviews">Pass True, if the user may add web page previews to their messages, implies can_send_media_messages</param>
        public RestrictChatMemberRequest(ChatId chatId, int userId, DateTime untilDate = default(DateTime),
            bool? canSendMessages = null, bool? canSendMediaMessages = null, bool? canSendOtherMessages = null,
            bool? canAddWebPagePreviews = null) : base("restrictChatMember",
                new Dictionary<string, object>()
                {
                    { "chat_id", chatId },
                    { "user_id", userId }
                })
        {
            if (untilDate != default(DateTime))
                Parameters.Add("until_date", untilDate);

            if (canSendMessages != null)
                Parameters.Add("can_send_messages", canSendMessages.Value);
            if (canSendMediaMessages != null)
                Parameters.Add("can_send_media_messages", canSendMediaMessages.Value);
            if (canSendOtherMessages != null)
                Parameters.Add("can_send_other_messages", canSendOtherMessages.Value);
            if (canAddWebPagePreviews != null)
                Parameters.Add("can_add_web_page_previews", canAddWebPagePreviews.Value);
        }
    }
}
