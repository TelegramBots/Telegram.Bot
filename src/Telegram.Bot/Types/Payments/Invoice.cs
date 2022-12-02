namespace Telegram.Bot.Types.Payments;

/// <summary>
/// This object contains basic information about an invoice.
/// </summary>
/// <seealso href="https://core.telegram.org/bots/api#invoice"/>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class Invoice
{
    /// <summary>
    /// Product name
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Title { get; set; } = default!;

    /// <summary>
    /// Product description
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Description { get; set; } = default!;

    /// <summary>
    /// Unique bot deep-linking parameter that can be used to generate this invoice
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string StartParameter { get; set; } = default!;

    /// <summary>
    /// Three-letter ISO 4217
    /// <a href="https://core.telegram.org/bots/payments#supported-currencies">currency</a> code
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Currency { get; set; } = default!;

    /// <summary>
    /// Total price in the <i>smallest units</i> of the
    /// <a href="https://core.telegram.org/bots/payments#supported-currencies">currency</a>
    /// (integer, <b>not</b> float/double).
    /// <para>
    /// For example, for a price of <c>US$ 1.45</c> pass <c>amount = 145</c>. See the <i>exp</i> parameter in
    /// <a href="https://core.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the
    /// number of digits past the decimal point for each currency (2 for the majority of currencies).
    /// </para>
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int TotalAmount { get; set; }
}
