namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.GetGameHighScoresAsync" /> method.
    /// </summary>
    public class GetGameHighScoresInlineParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier of the target user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Unique identifier of the inline message.
        /// </summary>
        public string InlineMessageId { get; set; }

        /// <summary>
        ///     Sets <see cref="UserId" /> property.
        /// </summary>
        /// <param name="userId">Unique identifier of the target user.</param>
        public GetGameHighScoresInlineParameters WithUserId(int userId)
        {
            UserId = userId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="InlineMessageId" /> property.
        /// </summary>
        /// <param name="inlineMessageId">Unique identifier of the inline message.</param>
        public GetGameHighScoresInlineParameters WithInlineMessageId(string inlineMessageId)
        {
            InlineMessageId = inlineMessageId;
            return this;
        }
    }
}
