// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Payments;

/// <summary>This object contains basic information about an invoice.</summary>
public partial class Invoice
{
    /// <summary>Product name</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Title { get; set; } = default!;

    /// <summary>Product description</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Description { get; set; } = default!;

    /// <summary>Unique bot deep-linking parameter that can be used to generate this invoice</summary>
    [JsonPropertyName("start_parameter")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string StartParameter { get; set; } = default!;

    /// <summary>Three-letter ISO 4217 <a href="https://core.telegram.org/bots/payments#supported-currencies">currency</a> code, or “XTR” for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Currency { get; set; } = default!;

    /// <summary>Total price in the <em>smallest units</em> of the currency (integer, <b>not</b> float/double). For example, for a price of <c>US$ 1.45</c> pass <c>amount = 145</c>. See the <em>exp</em> parameter in <a href="https://core.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies).</summary>
    [JsonPropertyName("total_amount")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long TotalAmount { get; set; }
}
