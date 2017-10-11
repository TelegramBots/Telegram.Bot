using System.Collections.Generic;
using Telegram.Bot.Types.Payments;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to answer a shipping query
    /// </summary>
    public class AnswerShippingQueryRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerShippingQueryRequest"/> class
        /// </summary>
        /// <param name="shippingQueryId">Unique identifier for the query to be answered</param>
        /// <param name="ok">Specify True if delivery to the specified address is possible and False if there are any problems</param>
        /// <param name="shippingOptions">Required if ok is True.</param>
        /// <param name="errorMessage">Required if ok is False. Error message in human readable form that explains why it is impossible to complete the order </param>
        public AnswerShippingQueryRequest(string shippingQueryId, bool ok,
            ShippingOption[] shippingOptions = null,
            string errorMessage = null) : base("answerShippingQuery",
                new Dictionary<string, object>
                {
                    {"shipping_query_id", shippingQueryId},
                    {"ok", ok}
                })
        {
            if (shippingOptions != null)
                Parameters.Add("shipping_options", shippingOptions);

            if (!ok)
                Parameters.Add("error_message", errorMessage);
        }
    }
}
