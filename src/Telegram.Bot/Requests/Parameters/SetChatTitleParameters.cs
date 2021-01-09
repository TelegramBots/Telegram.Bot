using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SetChatTitleAsync" /> method.
    /// </summary>
    public class SetChatTitleParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the target chat or username of the target channel
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     New chat title, 1-255 characters
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        public SetChatTitleParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Title" /> property.
        /// </summary>
        /// <param name="title">New chat title, 1-255 characters</param>
        public SetChatTitleParameters WithTitle(string title)
        {
            Title = title;
            return this;
        }
    }
}
