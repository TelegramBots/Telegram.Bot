// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Payments;

/// <summary>This object contains basic information about a refunded payment.</summary>
public partial class RefundedPayment
{
    /// <summary>Three-letter ISO 4217 <a href="https://core.telegram.org/bots/payments#supported-currencies">currency</a> code, or “XTR” for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>. Currently, always “XTR”</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Currency { get; set; } = default!;

    /// <summary>Total refunded price in the <em>smallest units</em> of the currency (integer, <b>not</b> float/double). For example, for a price of <c>US$ 1.45</c>, <c><see cref="TotalAmount">TotalAmount</see> = 145</c>. See the <em>exp</em> parameter in <a href="https://core.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies).</summary>
    [JsonPropertyName("total_amount")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long TotalAmount { get; set; }

    /// <summary>Bot-specified invoice payload</summary>
    [JsonPropertyName("invoice_payload")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string InvoicePayload { get; set; } = default!;

    /// <summary>Telegram payment identifier</summary>
    [JsonPropertyName("telegram_payment_charge_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string TelegramPaymentChargeId { get; set; } = default!;

    /// <summary><em>Optional</em>. Provider payment identifier</summary>
    [JsonPropertyName("provider_payment_charge_id")]
    public string? ProviderPaymentChargeId { get; set; }
}
