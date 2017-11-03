using Newtonsoft.Json;

namespace Telegram.Bot.Types.Payments
{
    /// <summary>
    /// This object represents information about an order.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class OrderInfo
    {
        /// <summary>
        /// Optional. User name
        /// </summary>
        [JsonProperty]
        public string Name { get; set; }

        /// <summary>
        /// Optional. User's phone number
        /// </summary>
        [JsonProperty]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Optional. User email
        /// </summary>
        [JsonProperty]
        public string Email { get; set; }

        /// <summary>
        /// Optional. User shipping address
        /// </summary>
        [JsonProperty]
        public ShippingAddress ShippingAddress { get; set; }
    }
}
