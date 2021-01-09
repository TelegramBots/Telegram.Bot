using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SendChatActionAsync" /> method.
    /// </summary>
    public class SendChatActionParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Type of action to broadcast. Choose one, depending on what the user is about to receive.
        /// </summary>
        public ChatAction ChatAction { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId"></param>
        public SendChatActionParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ChatAction" /> property.
        /// </summary>
        /// <param name="chatAction">Type of action to broadcast. Choose one, depending on what the user is about to receive.</param>
        public SendChatActionParameters WithChatAction(ChatAction chatAction)
        {
            ChatAction = chatAction;
            return this;
        }
    }
}