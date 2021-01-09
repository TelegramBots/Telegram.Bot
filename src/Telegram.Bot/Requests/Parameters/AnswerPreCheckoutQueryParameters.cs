namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.AnswerPreCheckoutQueryAsync" /> method.
    /// </summary>
    public class AnswerPreCheckoutQueryParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the query to be answered
        /// </summary>
        public string PreCheckoutQueryId { get; set; }

        /// <summary>
        ///     Sets <see cref="PreCheckoutQueryId" /> property.
        /// </summary>
        /// <param name="preCheckoutQueryId">Unique identifier for the query to be answered</param>
        public AnswerPreCheckoutQueryParameters WithPreCheckoutQueryId(string preCheckoutQueryId)
        {
            PreCheckoutQueryId = preCheckoutQueryId;
            return this;
        }
    }
}
