using System;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Kick a user from a group, a supergroup or a channel
    /// </summary>
    public class KickChatMemberRequest : RequestBase<bool>
    {
        /// <summary>
        /// Unique identifier for the target group or username of the target supergroup or channel
        /// </summary>
        public ChatId ChatId { get; }

        /// <summary>
        /// Unique identifier of the target user
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// Date when the user will be unbanned, unix time. If user is banned for more than 366 days or less than 30 seconds from the current time they are considered to be banned forever.
        /// </summary>
        public DateTime UntilDate { get; set; }

        /// <summary>
        /// Initializes a new request with chatId and userId
        /// </summary>
        /// <param name="chatId">Unique identifier for the target group or username of the target supergroup or channel</param>
        /// <param name="userId">Unique identifier of the target user</param>
        public KickChatMemberRequest(ChatId chatId, int userId, ITelegramBotJsonConverter jsonConverter)
            : base(jsonConverter, "kickChatMember")
        {
            ChatId = chatId;
            UserId = userId;
        }
    }
}
