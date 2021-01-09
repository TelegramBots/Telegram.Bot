using System.Collections.Generic;
using Telegram.Bot.Types.Payments;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.AnswerShippingQueryAsync" /> method.
    /// </summary>
    public class AnswerShippingQueryParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the query to be answered
        /// </summary>
        public string ShippingQueryId { get; set; }

        /// <summary>
        /// </summary>
        public IEnumerable<ShippingOption> ShippingOptions { get; set; }

        /// <summary>
        ///     Sets <see cref="ShippingQueryId" /> property.
        /// </summary>
        /// <param name="shippingQueryId">Unique identifier for the query to be answered</param>
        public AnswerShippingQueryParameters WithShippingQueryId(string shippingQueryId)
        {
            ShippingQueryId = shippingQueryId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ShippingOptions" /> property.
        /// </summary>
        public AnswerShippingQueryParameters WithShippingOptions(IEnumerable<ShippingOption> shippingOptions)
        {
            ShippingOptions = shippingOptions;
            return this;
        }
    }
}