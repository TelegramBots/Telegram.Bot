using System;
using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Kick a user from a group, a supergroup or a channel
    /// </summary>
    public sealed class KickChatMemberRequest : ChatIdRequestBase<bool>
    {
        /// <summary>
        /// Unique identifier of the target user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Date when the user will be unbanned, unix time. If user is banned for more than 366 days or less than 30 seconds from the current time they are considered to be banned forever.
        /// </summary>
        public DateTime UntilDate { get; set; }

        /// <summary>
        /// Initializes a new request with specified <see cref="ChatId"/> and <see cref="UserId"/>
        /// </summary>
        /// <param name="chatId">Unique identifier for the target group or username of the target supergroup or channel</param>
        /// <param name="userId">Unique identifier of the target user</param>
        public KickChatMemberRequest([NotNull] ChatId chatId, int userId)
            : base("kickChatMember")
        {
            ChatId = chatId;
            UserId = userId;
        }
    }
}
