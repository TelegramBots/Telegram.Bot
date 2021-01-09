using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.DeleteMessageAsync" /> method.
    /// </summary>
    public class DeleteMessageParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Unique identifier of the message to delete
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId"></param>
        public DeleteMessageParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="MessageId" /> property.
        /// </summary>
        /// <param name="messageId">Unique identifier of the message to delete</param>
        public DeleteMessageParameters WithMessageId(int messageId)
        {
            MessageId = messageId;
            return this;
        }
    }
}
