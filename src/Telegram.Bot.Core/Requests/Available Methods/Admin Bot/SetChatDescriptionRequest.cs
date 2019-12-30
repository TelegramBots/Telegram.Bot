using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
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
        public ChatId ChatId { get; }

        /// <summary>
        /// New chat Description, 0-255 characters
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Initializes a new request with chatId and new title
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="description">New chat Description, 0-255 characters</param>
        public SetChatDescriptionRequest(ChatId chatId, ITelegramBotJsonConverter jsonConverter,
                                         string description = default)
            : base(jsonConverter, "setChatDescription")
        {
            ChatId = chatId;
            Description = description;
        }
    }
}
