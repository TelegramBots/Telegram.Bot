using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.GetChatAdministratorsAsync" /> method.
    /// </summary>
    public class GetChatAdministratorsParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId"></param>
        public GetChatAdministratorsParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }
    }
}
