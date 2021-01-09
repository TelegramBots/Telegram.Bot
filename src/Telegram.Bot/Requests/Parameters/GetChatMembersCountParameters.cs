using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.GetChatMembersCountAsync" /> method.
    /// </summary>
    public class GetChatMembersCountParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId"></param>
        public GetChatMembersCountParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }
    }
}