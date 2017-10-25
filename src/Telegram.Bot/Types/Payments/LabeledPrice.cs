using Newtonsoft.Json;

namespace Telegram.Bot.Types.Payments
{
    /// <summary>
    /// This object represents a portion of the price for goods or services.
    /// </summary>
    /// <see href="https://core.telegram.org/bots/api#labeledprice"/>
    [JsonObject(MemberSerialization.OptIn)]
    public class LabeledPrice
    {
        /// <summary>
        /// Portion label
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Label { get; set; }

        /// <summary>
        /// Price of the product in the smallest units of the <see href="https://core.telegram.org/bots/payments#supported-currencies">currency</see>.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int Amount { get; set; }
    }
}
