using Newtonsoft.Json;

namespace Telegram.Bot.Types.Payments
{
    /// <summary>
    /// This object contains information about an incoming shipping query.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ShippingQuery
    {
        /// <summary>
        /// Unique query identifier
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        /// <summary>
        /// User who sent the query
        /// </summary>
        [JsonProperty("from", Required = Required.Always)]
        public User From { get; set; }

        /// <summary>
        /// Bot specified invoice payload
        /// </summary>
        [JsonProperty("invoice_payload", Required = Required.Always)]
        public string InvoicePayload { get; set; }

        /// <summary>
        /// User specified shipping address
        /// </summary>
        [JsonProperty("shipping_address", Required = Required.Always)]
        public ShippingAddress ShippingAddress { get; set; }
    }
}
