namespace Telegram.Bot.Types.Payments;

/// <summary>
/// This object contains basic information about a successful payment.
/// </summary>
public class SuccessfulPayment
{
    /// <summary>
    /// Three-letter ISO 4217
    /// <a href="https://core.telegram.org/bots/payments#supported-currencies">currency</a> code
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Currency { get; set; } = default!;

    /// <summary>
    /// Total price in the <i>smallest units</i> of the
    /// <a href="https://core.telegram.org/bots/payments#supported-currencies">currency</a>
    /// (integer, <b>not</b> float/double).
    /// <para>
    /// For example, for a price of <c>US$ 1.45</c> pass <c>amount = 145</c>. See the <i>exp</i> parameter
    /// in <a href="https://core.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows
    /// the number of digits past the decimal point for each currency (2 for the majority of currencies).
    /// </para>
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int TotalAmount { get; set; }

    /// <summary>
    /// Bot specified invoice payload
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string InvoicePayload { get; set; } = default!;

    /// <summary>
    /// Optional. Identifier of the shipping option chosen by the user
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ShippingOptionId { get; set; }

    /// <summary>
    /// Optional. Order info provided by the user
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public OrderInfo? OrderInfo { get; set; }

    /// <summary>
    /// Telegram payment identifier
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string TelegramPaymentChargeId { get; set; } = default!;

    /// <summary>
    /// Provider payment identifier
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string ProviderPaymentChargeId { get; set; } = default!;
}
