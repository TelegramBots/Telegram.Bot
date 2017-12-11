using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Tell the user that something is happening on the bot's side
    /// </summary>
    public class SendChatActionRequest : RequestBase<bool>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// Type of action to broadcast
        /// </summary>
        public ChatAction Action { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public SendChatActionRequest()
            : base("sendChatAction")
        { }

        /// <summary>
        /// Initializes a new request chatId and action
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="action">Type of action to broadcast</param>
        public SendChatActionRequest(ChatId chatId, ChatAction action)
            : this()
        {
            ChatId = chatId;
            Action = action;
        }
    }
}
