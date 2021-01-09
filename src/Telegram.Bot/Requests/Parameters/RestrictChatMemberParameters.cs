using System;
using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.RestrictChatMemberAsync" /> method.
    /// </summary>
    public class RestrictChatMemberParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the target chat or username of the target supergroup
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Unique identifier of the target user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     New user permissions
        /// </summary>
        public ChatPermissions Permissions { get; set; }

        /// <summary>
        /// </summary>
        public DateTime UntilDate { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
        public RestrictChatMemberParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="UserId" /> property.
        /// </summary>
        /// <param name="userId">Unique identifier of the target user</param>
        public RestrictChatMemberParameters WithUserId(int userId)
        {
            UserId = userId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Permissions" /> property.
        /// </summary>
        /// <param name="permissions">New user permissions</param>
        public RestrictChatMemberParameters WithPermissions(ChatPermissions permissions)
        {
            Permissions = permissions;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="UntilDate" /> property.
        /// </summary>
        /// <param name="untilDate"></param>
        public RestrictChatMemberParameters WithUntilDate(DateTime untilDate)
        {
            UntilDate = untilDate;
            return this;
        }
    }
}
