// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Payments;

/// <summary>This object contains basic information about a successful payment. Note that if the buyer initiates a chargeback with the relevant payment provider following this transaction, the funds may be debited from your balance. This is outside of Telegram's control.</summary>
public partial class SuccessfulPayment
{
    /// <summary>Three-letter ISO 4217 <a href="https://core.telegram.org/bots/payments#supported-currencies">currency</a> code, or “XTR” for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Currency { get; set; } = default!;

    /// <summary>Total price in the <em>smallest units</em> of the currency (integer, <b>not</b> float/double). For example, for a price of <c>US$ 1.45</c> pass <c>amount = 145</c>. See the <em>exp</em> parameter in <a href="https://core.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies).</summary>
    [JsonPropertyName("total_amount")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long TotalAmount { get; set; }

    /// <summary>Bot-specified invoice payload</summary>
    [JsonPropertyName("invoice_payload")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string InvoicePayload { get; set; } = default!;

    /// <summary><em>Optional</em>. Expiration date of the subscription,; for recurring payments only</summary>
    [JsonPropertyName("subscription_expiration_date")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? SubscriptionExpirationDate { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the payment is a recurring payment for a subscription</summary>
    [JsonPropertyName("is_recurring")]
    public bool IsRecurring { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the payment is the first payment for a subscription</summary>
    [JsonPropertyName("is_first_recurring")]
    public bool IsFirstRecurring { get; set; }

    /// <summary><em>Optional</em>. Identifier of the shipping option chosen by the user</summary>
    [JsonPropertyName("shipping_option_id")]
    public string? ShippingOptionId { get; set; }

    /// <summary><em>Optional</em>. Order information provided by the user</summary>
    [JsonPropertyName("order_info")]
    public OrderInfo? OrderInfo { get; set; }

    /// <summary>Telegram payment identifier</summary>
    [JsonPropertyName("telegram_payment_charge_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string TelegramPaymentChargeId { get; set; } = default!;

    /// <summary>Provider payment identifier</summary>
    [JsonPropertyName("provider_payment_charge_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string ProviderPaymentChargeId { get; set; } = default!;
}
