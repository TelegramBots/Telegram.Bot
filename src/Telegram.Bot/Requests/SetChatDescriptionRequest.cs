using Telegram.Bot.Types;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Change the description of a supergroup or a channel. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
    /// </summary>
    public class SetChatDescriptionRequest : RequestBase<bool>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// New chat Description, 0-255 characters
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public SetChatDescriptionRequest()
            : base("setChatDescription")
        { }

        /// <summary>
        /// Initializes a new request with chatId and new title
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="description">New chat description, 0-255 characters</param>
        public SetChatDescriptionRequest(ChatId chatId, string description)
            : this()
        {
            ChatId = chatId;
            Description = description;
        }
    }
}
