using Telegram.Bot.Types;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Get information about a member of a chat
    /// </summary>
    public class GetChatMemberRequest : RequestBase<ChatMember>
    {
        /// <summary>
        /// Unique identifier for the target group or username of the target supergroup or channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// Unique identifier of the target user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public GetChatMemberRequest()
            : base("getChatMember")
        { }

        /// <summary>
        /// Initializes a new request with chatId and userId
        /// </summary>
        /// <param name="chatId">Unique identifier for the target group or username of the target supergroup or channel</param>
        /// <param name="userId">Unique identifier of the target user</param>
        public GetChatMemberRequest(ChatId chatId, int userId)
            : this()
        {
            ChatId = chatId;
            UserId = userId;
        }
    }
}
