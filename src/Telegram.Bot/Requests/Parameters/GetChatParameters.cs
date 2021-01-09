using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.GetChatAsync" /> method.
    /// </summary>
    public class GetChatParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId"></param>
        public GetChatParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }
    }
}
