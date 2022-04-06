using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.Payments;

/// <summary>
/// This object represents a portion of the price for goods or services.
/// </summary>
/// <see href="https://core.telegram.org/bots/api#labeledprice"/>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class LabeledPrice
{
    /// <summary>
    /// Portion label
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Label { get; set; }

    /// <summary>
    /// Price of the product in the <i>smallest units</i> of the <see href="https://core.telegram.org/bots/payments#supported-currencies">currency</see> (integer, <b>not</b> float/double).
    /// <para>For example, for a price of <c>US$ 1.45</c> pass <c>amount = 145</c>. See the <i>exp</i> parameter in <see href="https://core.telegram.org/bots/payments/currencies.json">currencies.json</see>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies).</para>
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Amount { get; set; }

    /// <summary>
    /// Initializes an instance of <see cref="LabeledPrice"/>
    /// </summary>
    /// <param name="label">Portion label</param>
    /// <param name="amount">Price of the product</param>
    [JsonConstructor]
    public LabeledPrice(string label, int amount)
    {
        Label = label;
        Amount = amount;
    }
}