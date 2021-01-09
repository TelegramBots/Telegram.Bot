namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.GetGameHighScoresAsync" /> method.
    /// </summary>
    public class GetGameHighScoresParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier of the target user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// </summary>
        public long ChatId { get; set; }

        /// <summary>
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        ///     Sets <see cref="UserId" /> property.
        /// </summary>
        /// <param name="userId">Unique identifier of the target user.</param>
        public GetGameHighScoresParameters WithUserId(int userId)
        {
            UserId = userId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        public GetGameHighScoresParameters WithChatId(long chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="MessageId" /> property.
        /// </summary>
        public GetGameHighScoresParameters WithMessageId(int messageId)
        {
            MessageId = messageId;
            return this;
        }
    }
}
