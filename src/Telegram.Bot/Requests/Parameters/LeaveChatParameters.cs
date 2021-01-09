using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.LeaveChatAsync" /> method.
    /// </summary>
    public class LeaveChatParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId"></param>
        public LeaveChatParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }
    }
}