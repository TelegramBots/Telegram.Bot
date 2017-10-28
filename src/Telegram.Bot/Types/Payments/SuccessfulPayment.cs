using Newtonsoft.Json;

namespace Telegram.Bot.Types.Payments
{
    /// <summary>
    /// This object contains basic information about a successful payment.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class SuccessfulPayment
    {
        /// <summary>
        /// Three-letter ISO 4217 currency code
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Currency { get; set; }

        /// <summary>
        /// Total price in the smallest units of the currency.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int TotalAmount { get; set; }

        /// <summary>
        /// Bot specified invoice payload
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string InvoicePayload { get; set; }

        /// <summary>
        /// Optional. Identifier of the shipping option chosen by the user
        /// </summary>
        [JsonProperty]
        public string ShippingOptionId { get; set; }

        /// <summary>
        /// Optional. Order info provided by the user
        /// </summary>
        [JsonProperty]
        public OrderInfo OrderInfo { get; set; }

        /// <summary>
        /// Telegram payment identifier
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string TelegramPaymentChargeId { get; set; }

        /// <summary>
        /// Provider payment identifier
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string ProviderPaymentChargeId { get; set; }
    }
}
