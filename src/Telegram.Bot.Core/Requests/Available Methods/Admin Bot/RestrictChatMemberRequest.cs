using System;
using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Restrict a user in a supergroup. The bot must be an administrator in the supergroup for this to work and must have the appropriate admin rights. Pass True for all boolean parameters to lift restrictions from a user.
    /// </summary>
    public sealed class RestrictChatMemberRequest : ChatIdRequestBase<bool>
    {
        /// <summary>
        /// Unique identifier of the target user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Describes the permissions to set for specified user
        /// </summary>
        [NotNull]
        public ChatPermissions Permissions { get; set; } = new ChatPermissions();

        /// <summary>
        /// Date when restrictions will be lifted for the user, unix time. If user is restricted for more than 366 days or less than 30 seconds from the current time, they are considered to be restricted forever.
        /// </summary>
        public DateTime UntilDate { get; set; }

        /// <summary>
        /// Initializes a new request with both <see cref="ChatId"/> and <see cref="UserId"/> set to 0
        /// </summary>
        public RestrictChatMemberRequest() : this(0, 0)
        {
        }

        /// <summary>
        /// Initializes a new request with specified <see cref="ChatId"/> and <see cref="UserId"/>
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
        /// <param name="userId">Unique identifier of the target user</param>
        public RestrictChatMemberRequest([NotNull] ChatId chatId, int userId)
            : base("restrictChatMember")
        {
            ChatId = chatId;
            UserId = userId;
        }
    }
}
