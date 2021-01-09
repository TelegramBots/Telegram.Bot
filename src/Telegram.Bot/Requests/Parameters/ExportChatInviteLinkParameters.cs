using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.ExportChatInviteLinkAsync" /> method.
    /// </summary>
    public class ExportChatInviteLinkParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the target chat or username of the target channel
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        public ExportChatInviteLinkParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }
    }
}