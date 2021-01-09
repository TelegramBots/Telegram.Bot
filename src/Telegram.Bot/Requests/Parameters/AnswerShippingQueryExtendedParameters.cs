namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.AnswerShippingQueryAsync" /> method.
    /// </summary>
    public class AnswerShippingQueryExtendedParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the query to be answered
        /// </summary>
        public string ShippingQueryId { get; set; }

        /// <summary>
        ///     Required if OK is False. Error message in human readable form that explains why it is impossible to complete the
        ///     order
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     Sets <see cref="ShippingQueryId" /> property.
        /// </summary>
        /// <param name="shippingQueryId">Unique identifier for the query to be answered</param>
        public AnswerShippingQueryExtendedParameters WithShippingQueryId(string shippingQueryId)
        {
            ShippingQueryId = shippingQueryId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ErrorMessage" /> property.
        /// </summary>
        /// <param name="errorMessage">
        ///     Required if OK is False. Error message in human readable form that explains why it is
        ///     impossible to complete the order
        /// </param>
        public AnswerShippingQueryExtendedParameters WithErrorMessage(string errorMessage)
        {
            ErrorMessage = errorMessage;
            return this;
        }
    }
}
