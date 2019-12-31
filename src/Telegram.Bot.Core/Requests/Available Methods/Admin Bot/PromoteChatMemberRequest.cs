﻿using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Promote or demote a user in a supergroup or a channel. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights. Pass False for all boolean parameters to demote a user.
    /// </summary>
    public class PromoteChatMemberRequest : RequestBase<bool>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; }

        /// <summary>
        /// Unique identifier of the target user
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// Pass True, if the administrator can change chat title, photo and other settings
        /// </summary>
        public bool? CanChangeInfo { get; set; }

        /// <summary>
        /// Pass True, if the administrator can create channel posts, channels only
        /// </summary>
        public bool? CanPostMessages { get; set; }

        /// <summary>
        /// Pass True, if the administrator can edit messages of other users and can pin messages, channels only
        /// </summary>
        public bool? CanEditMessages { get; set; }

        /// <summary>
        /// Pass True, if the administrator can delete messages of other users
        /// </summary>
        public bool? CanDeleteMessages { get; set; }

        /// <summary>
        /// Pass True, if the administrator can invite new users to the chat
        /// </summary>
        public bool? CanInviteUsers { get; set; }

        /// <summary>
        /// Pass True, if the administrator can restrict, ban or unban chat members
        /// </summary>
        public bool? CanRestrictMembers { get; set; }

        /// <summary>
        /// Pass True, if the administrator can pin messages, supergroups only
        /// </summary>
        public bool? CanPinMessages { get; set; }

        /// <summary>
        /// Pass True, if the administrator can add new administrators with a subset of his own privileges or demote administrators that he has promoted, directly or indirectly (promoted by administrators that were appointed by him)
        /// </summary>
        public bool? CanPromoteMembers { get; set; }

        /// <summary>
        /// Initializes a new request with chatId and userId
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="userId">Unique identifier of the target user</param>
        public PromoteChatMemberRequest(ChatId chatId, int userId)
            : base("promoteChatMember")
        {
            ChatId = chatId;
            UserId = userId;
        }
    }
}
