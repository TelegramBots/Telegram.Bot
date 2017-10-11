using System;
using System.Collections.Generic;
using System.Text;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to promote a chat member
    /// </summary>
    public class PromoteChatMemberRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PromoteChatMemberRequest"/> class
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="userId">Unique identifier of the target user</param>
        /// <param name="canChangeInfo">Pass True, if the administrator can change chat title, photo and other settings</param>
        /// <param name="canPostMessages">Pass True, if the administrator can create channel posts, channels only</param>
        /// <param name="canEditMessages">Pass True, if the administrator can edit messages of other users, channels only</param>
        /// <param name="canDeleteMessages">Pass True, if the administrator can delete messages of other users</param>
        /// <param name="canInviteUsers">Pass True, if the administrator can invite new users to the chat</param>
        /// <param name="canRestrictMembers">Pass True, if the administrator can restrict, ban or unban chat members</param>
        /// <param name="canPinMessages">Pass True, if the administrator can pin messages, supergroups only</param>
        /// <param name="canPromoteMembers">Pass True, if the administrator can add new administrators with a subset of his own privileges or demote administrators that he has promoted, directly or indirectly (promoted by administrators that were appointed by him)</param>
        public PromoteChatMemberRequest(ChatId chatId, int userId, bool? canChangeInfo = null,
            bool? canPostMessages = null, bool? canEditMessages = null, bool? canDeleteMessages = null,
            bool? canInviteUsers = null, bool? canRestrictMembers = null, bool? canPinMessages = null,
            bool? canPromoteMembers = null) : base("promoteChatMember",
                new Dictionary<string, object>()
                {
                    { "chat_id", chatId },
                    { "user_id", userId }
                })
        {
            if (canChangeInfo != null)
                Parameters.Add("can_change_info", canChangeInfo.Value);
            if (canPostMessages != null)
                Parameters.Add("can_post_messages", canPostMessages.Value);
            if (canEditMessages != null)
                Parameters.Add("can_edit_messages", canEditMessages.Value);
            if (canDeleteMessages != null)
                Parameters.Add("can_delete_messages", canDeleteMessages.Value);
            if (canInviteUsers != null)
                Parameters.Add("can_invite_users", canInviteUsers.Value);
            if (canRestrictMembers != null)
                Parameters.Add("can_restrict_members", canRestrictMembers.Value);
            if (canPinMessages != null)
                Parameters.Add("can_pin_messages", canPinMessages.Value);
            if (canPromoteMembers != null)
                Parameters.Add("can_promote_members", canPromoteMembers.Value);
        }
    }
}
