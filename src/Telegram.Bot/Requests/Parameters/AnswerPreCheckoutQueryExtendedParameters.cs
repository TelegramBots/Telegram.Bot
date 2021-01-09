namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.AnswerPreCheckoutQueryAsync" /> method.
    /// </summary>
    public class AnswerPreCheckoutQueryExtendedParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the query to be answered
        /// </summary>
        public string PreCheckoutQueryId { get; set; }

        /// <summary>
        ///     Required if OK is False. Error message in human readable form that explains the reason for failure to proceed with
        ///     the checkout
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     Sets <see cref="PreCheckoutQueryId" /> property.
        /// </summary>
        /// <param name="preCheckoutQueryId">Unique identifier for the query to be answered</param>
        public AnswerPreCheckoutQueryExtendedParameters WithPreCheckoutQueryId(string preCheckoutQueryId)
        {
            PreCheckoutQueryId = preCheckoutQueryId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ErrorMessage" /> property.
        /// </summary>
        /// <param name="errorMessage">
        ///     Required if OK is False. Error message in human readable form that explains the reason for
        ///     failure to proceed with the checkout
        /// </param>
        public AnswerPreCheckoutQueryExtendedParameters WithErrorMessage(string errorMessage)
        {
            ErrorMessage = errorMessage;
            return this;
        }
    }
}