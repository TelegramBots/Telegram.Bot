using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.UnpinChatMessageAsync" /> method.
    /// </summary>
    public class UnpinChatMessageParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the target chat or username of the target supergroup
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
        public UnpinChatMessageParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }
    }
}