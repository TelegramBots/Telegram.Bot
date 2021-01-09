using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SetChatDescriptionAsync" /> method.
    /// </summary>
    public class SetChatDescriptionParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the target chat or username of the target channel
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     New chat description, 0-255 characters. Defaults to an empty string, which would clear the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        public SetChatDescriptionParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Description" /> property.
        /// </summary>
        /// <param name="description">
        ///     New chat description, 0-255 characters. Defaults to an empty string, which would clear the
        ///     description.
        /// </param>
        public SetChatDescriptionParameters WithDescription(string description)
        {
            Description = description;
            return this;
        }
    }
}