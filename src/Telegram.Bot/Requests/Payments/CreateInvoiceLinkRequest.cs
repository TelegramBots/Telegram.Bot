// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to create a link for an invoice.<para>Returns: The created invoice link as <em>String</em> on success.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class CreateInvoiceLinkRequest() : RequestBase<string>("createInvoiceLink"), IBusinessConnectable
{
    /// <summary>Product name, 1-32 characters</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; set; }

    /// <summary>Product description, 1-255 characters</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Description { get; set; }

    /// <summary>Bot-defined invoice payload, 1-128 bytes. This will not be displayed to the user, use it for your internal processes.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Payload { get; set; }

    /// <summary>Three-letter ISO 4217 currency code, see <a href="https://core.telegram.org/bots/payments#supported-currencies">more on currencies</a>. Pass “XTR” for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Currency { get; set; }

    /// <summary>Price breakdown, a list of components (e.g. product price, tax, discount, delivery cost, delivery tax, bonus, etc.). Must contain exactly one item for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<LabeledPrice> Prices { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the link will be created. For payments in <a href="https://t.me/BotNews/90">Telegram Stars</a> only.</summary>
    [JsonPropertyName("business_connection_id")]
    public string? BusinessConnectionId { get; set; }

    /// <summary>Payment provider token, obtained via <a href="https://t.me/botfather">@BotFather</a>. Pass an empty string for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    [JsonPropertyName("provider_token")]
    public string? ProviderToken { get; set; }

    /// <summary>The number of seconds the subscription will be active for before the next payment. The currency must be set to “XTR” (Telegram Stars) if the parameter is used. Currently, it must always be 2592000 (30 days) if specified. Any number of subscriptions can be active for a given bot at the same time, including multiple concurrent subscriptions from the same user. Subscription price must no exceed 10000 Telegram Stars.</summary>
    [JsonPropertyName("subscription_period")]
    public int? SubscriptionPeriod { get; set; }

    /// <summary>The maximum accepted amount for tips in the <em>smallest units</em> of the currency (integer, <b>not</b> float/double). For example, for a maximum tip of <c>US$ 1.45</c> pass <c><see cref="MaxTipAmount">MaxTipAmount</see> = 145</c>. See the <em>exp</em> parameter in <a href="https://core.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies). Defaults to 0. Not supported for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    [JsonPropertyName("max_tip_amount")]
    public long? MaxTipAmount { get; set; }

    /// <summary>A array of suggested amounts of tips in the <em>smallest units</em> of the currency (integer, <b>not</b> float/double). At most 4 suggested tip amounts can be specified. The suggested tip amounts must be positive, passed in a strictly increased order and must not exceed <see cref="MaxTipAmount">MaxTipAmount</see>.</summary>
    [JsonPropertyName("suggested_tip_amounts")]
    public IEnumerable<int>? SuggestedTipAmounts { get; set; }

    /// <summary>JSON-serialized data about the invoice, which will be shared with the payment provider. A detailed description of required fields should be provided by the payment provider.</summary>
    [JsonPropertyName("provider_data")]
    public string? ProviderData { get; set; }

    /// <summary>URL of the product photo for the invoice. Can be a photo of the goods or a marketing image for a service.</summary>
    [JsonPropertyName("photo_url")]
    public string? PhotoUrl { get; set; }

    /// <summary>Photo size in bytes</summary>
    [JsonPropertyName("photo_size")]
    public int? PhotoSize { get; set; }

    /// <summary>Photo width</summary>
    [JsonPropertyName("photo_width")]
    public int? PhotoWidth { get; set; }

    /// <summary>Photo height</summary>
    [JsonPropertyName("photo_height")]
    public int? PhotoHeight { get; set; }

    /// <summary>Pass <see langword="true"/> if you require the user's full name to complete the order. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    [JsonPropertyName("need_name")]
    public bool NeedName { get; set; }

    /// <summary>Pass <see langword="true"/> if you require the user's phone number to complete the order. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    [JsonPropertyName("need_phone_number")]
    public bool NeedPhoneNumber { get; set; }

    /// <summary>Pass <see langword="true"/> if you require the user's email address to complete the order. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    [JsonPropertyName("need_email")]
    public bool NeedEmail { get; set; }

    /// <summary>Pass <see langword="true"/> if you require the user's shipping address to complete the order. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    [JsonPropertyName("need_shipping_address")]
    public bool NeedShippingAddress { get; set; }

    /// <summary>Pass <see langword="true"/> if the user's phone number should be sent to the provider. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    [JsonPropertyName("send_phone_number_to_provider")]
    public bool SendPhoneNumberToProvider { get; set; }

    /// <summary>Pass <see langword="true"/> if the user's email address should be sent to the provider. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    [JsonPropertyName("send_email_to_provider")]
    public bool SendEmailToProvider { get; set; }

    /// <summary>Pass <see langword="true"/> if the final price depends on the shipping method. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    [JsonPropertyName("is_flexible")]
    public bool IsFlexible { get; set; }
}
