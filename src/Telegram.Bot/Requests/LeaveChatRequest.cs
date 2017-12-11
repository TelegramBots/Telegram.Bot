using Telegram.Bot.Types;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Leave a group, supergroup or channel
    /// </summary>
    public class LeaveChatRequest : RequestBase<bool>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public LeaveChatRequest()
            : base("leaveChat")
        { }

        /// <summary>
        /// Initializes a new request with chatId
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        public LeaveChatRequest(ChatId chatId)
            : this()
        {
            ChatId = chatId;
        }
    }
}
