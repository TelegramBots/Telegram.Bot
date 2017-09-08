using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to answer a pre-checkout query
    /// </summary>
    public class AnswerPreCheckoutQueryRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerPreCheckoutQueryRequest"/> class
        /// </summary>
        /// <param name="preCheckoutQueryId">Unique identifier for the query to be answered</param>
        /// <param name="ok">Specify True if everything is alright</param>
        /// <param name="errorMessage">Required if ok is False. Error message in human readable form that explains the reason for failure to proceed with the checkout</param>
        public AnswerPreCheckoutQueryRequest(string preCheckoutQueryId, bool ok,
            string errorMessage = null) : base("answerPreCheckoutQuery",
                new Dictionary<string, object>
                {
                    {"pre_checkout_query_id", preCheckoutQueryId},
                    {"ok", ok}
                })
        {
            if (!ok)
                Parameters.Add("error_message", errorMessage);
        }
    }
}
