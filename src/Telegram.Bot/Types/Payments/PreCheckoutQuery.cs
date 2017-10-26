using Newtonsoft.Json;

namespace Telegram.Bot.Types.Payments
{
    /// <summary>
    /// This object contains information about an incoming pre-checkout query
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class PreCheckoutQuery
    {
        /// <summary>
        /// Unique query identifier
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }

        /// <summary>
        /// User who sent the query
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public User From { get; set; }

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
    }
}