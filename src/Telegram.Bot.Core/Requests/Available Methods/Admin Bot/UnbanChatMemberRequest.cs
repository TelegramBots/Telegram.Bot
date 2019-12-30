using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Unban a previously kicked user in a supergroup or channel
    /// </summary>
    public class UnbanChatMemberRequest : RequestBase<bool>
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
        /// Initializes a new request with chatId and userId
        /// </summary>
        /// <param name="chatId">Unique identifier for the target group or username of the target supergroup or channel</param>
        /// <param name="userId">Unique identifier of the target user</param>
        public UnbanChatMemberRequest(ChatId chatId, int userId, ITelegramBotJsonConverter jsonConverter)
            : base(jsonConverter, "unbanChatMember")
        {
            ChatId = chatId;
            UserId = userId;
        }
    }
}
