using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.PromoteChatMemberAsync" /> method.
    /// </summary>
    public class PromoteChatMemberParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the target chat or username of the target channel
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Unique identifier of the target user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Pass True, if the administrator can change chat title, photo and other settings
        /// </summary>
        public bool? CanChangeInfo { get; set; }

        /// <summary>
        ///     Pass True, if the administrator can create channel posts, channels only
        /// </summary>
        public bool? CanPostMessages { get; set; }

        /// <summary>
        ///     Pass True, if the administrator can edit messages of other users, channels only
        /// </summary>
        public bool? CanEditMessages { get; set; }

        /// <summary>
        ///     Pass True, if the administrator can delete messages of other users
        /// </summary>
        public bool? CanDeleteMessages { get; set; }

        /// <summary>
        ///     Pass True, if the administrator can invite new users to the chat
        /// </summary>
        public bool? CanInviteUsers { get; set; }

        /// <summary>
        ///     Pass True, if the administrator can restrict, ban or unban chat members
        /// </summary>
        public bool? CanRestrictMembers { get; set; }

        /// <summary>
        ///     Pass True, if the administrator can pin messages, supergroups only
        /// </summary>
        public bool? CanPinMessages { get; set; }

        /// <summary>
        ///     Pass True, if the administrator can add new administrators with a subset of his own privileges or demote
        ///     administrators that he has promoted, directly or indirectly (promoted by administrators that were appointed by him)
        /// </summary>
        public bool? CanPromoteMembers { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        public PromoteChatMemberParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="UserId" /> property.
        /// </summary>
        /// <param name="userId">Unique identifier of the target user</param>
        public PromoteChatMemberParameters WithUserId(int userId)
        {
            UserId = userId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="CanChangeInfo" /> property.
        /// </summary>
        /// <param name="canChangeInfo">Pass True, if the administrator can change chat title, photo and other settings</param>
        public PromoteChatMemberParameters WithCanChangeInfo(bool? canChangeInfo)
        {
            CanChangeInfo = canChangeInfo;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="CanPostMessages" /> property.
        /// </summary>
        /// <param name="canPostMessages">Pass True, if the administrator can create channel posts, channels only</param>
        public PromoteChatMemberParameters WithCanPostMessages(bool? canPostMessages)
        {
            CanPostMessages = canPostMessages;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="CanEditMessages" /> property.
        /// </summary>
        /// <param name="canEditMessages">Pass True, if the administrator can edit messages of other users, channels only</param>
        public PromoteChatMemberParameters WithCanEditMessages(bool? canEditMessages)
        {
            CanEditMessages = canEditMessages;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="CanDeleteMessages" /> property.
        /// </summary>
        /// <param name="canDeleteMessages">Pass True, if the administrator can delete messages of other users</param>
        public PromoteChatMemberParameters WithCanDeleteMessages(bool? canDeleteMessages)
        {
            CanDeleteMessages = canDeleteMessages;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="CanInviteUsers" /> property.
        /// </summary>
        /// <param name="canInviteUsers">Pass True, if the administrator can invite new users to the chat</param>
        public PromoteChatMemberParameters WithCanInviteUsers(bool? canInviteUsers)
        {
            CanInviteUsers = canInviteUsers;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="CanRestrictMembers" /> property.
        /// </summary>
        /// <param name="canRestrictMembers">Pass True, if the administrator can restrict, ban or unban chat members</param>
        public PromoteChatMemberParameters WithCanRestrictMembers(bool? canRestrictMembers)
        {
            CanRestrictMembers = canRestrictMembers;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="CanPinMessages" /> property.
        /// </summary>
        /// <param name="canPinMessages">Pass True, if the administrator can pin messages, supergroups only</param>
        public PromoteChatMemberParameters WithCanPinMessages(bool? canPinMessages)
        {
            CanPinMessages = canPinMessages;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="CanPromoteMembers" /> property.
        /// </summary>
        /// <param name="canPromoteMembers">
        ///     Pass True, if the administrator can add new administrators with a subset of his own
        ///     privileges or demote administrators that he has promoted, directly or indirectly (promoted by administrators that
        ///     were appointed by him)
        /// </param>
        public PromoteChatMemberParameters WithCanPromoteMembers(bool? canPromoteMembers)
        {
            CanPromoteMembers = canPromoteMembers;
            return this;
        }
    }
}
