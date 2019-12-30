namespace Telegram.Bot.Types.Payments
{
    /// <summary>
    /// This object contains basic information about a successful payment.
    /// </summary>
    public class SuccessfulPayment
    {
        /// <summary>
        /// Three-letter ISO 4217 currency code
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Total price in the smallest units of the currency.
        /// </summary>
        public int TotalAmount { get; set; }

        /// <summary>
        /// Bot specified invoice payload
        /// </summary>
        public string InvoicePayload { get; set; }

        /// <summary>
        /// Optional. Identifier of the shipping option chosen by the user
        /// </summary>
        public string ShippingOptionId { get; set; }

        /// <summary>
        /// Optional. Order info provided by the user
        /// </summary>
        public OrderInfo OrderInfo { get; set; }

        /// <summary>
        /// Telegram payment identifier
        /// </summary>
        public string TelegramPaymentChargeId { get; set; }

        /// <summary>
        /// Provider payment identifier
        /// </summary>
        public string ProviderPaymentChargeId { get; set; }
    }
}
