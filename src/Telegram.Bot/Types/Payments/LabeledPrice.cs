// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Payments;

/// <summary>This object represents a portion of the price for goods or services.</summary>
public partial class LabeledPrice
{
    /// <summary>Portion label</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Label { get; set; }

    /// <summary>Price of the product in the <em>smallest units</em> of the <a href="https://core.telegram.org/bots/payments#supported-currencies">currency</a> (integer, <b>not</b> float/double). For example, for a price of <c>US$ 1.45</c> pass <c>amount = 145</c>. See the <em>exp</em> parameter in <a href="https://core.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies).</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long Amount { get; set; }

    /// <summary>Initializes an instance of <see cref="LabeledPrice"/></summary>
    /// <param name="label">Portion label</param>
    /// <param name="amount">Price of the product in the <em>smallest units</em> of the <a href="https://core.telegram.org/bots/payments#supported-currencies">currency</a> (integer, <b>not</b> float/double). For example, for a price of <c>US$ 1.45</c> pass <c>amount = 145</c>. See the <em>exp</em> parameter in <a href="https://core.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies).</param>
    [SetsRequiredMembers]
    public LabeledPrice(string label, long amount)
    {
        Label = label;
        Amount = amount;
    }

    /// <summary>Instantiates a new <see cref="LabeledPrice"/></summary>
    public LabeledPrice() { }
}
