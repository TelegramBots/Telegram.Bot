namespace Telegram.Bot.Requests;

/// <summary>Use this method to create a link for an invoice.<para>Returns: The created invoice link as <em>String</em> on success.</para></summary>
public partial class CreateInvoiceLinkRequest : RequestBase<string>
{
    /// <summary>Product name, 1-32 characters</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; set; }

    /// <summary>Product description, 1-255 characters</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Description { get; set; }

    /// <summary>Bot-defined invoice payload, 1-128 bytes. This will not be displayed to the user, use for your internal processes.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Payload { get; set; }

    /// <summary>Payment provider token, obtained via <a href="https://t.me/botfather">@BotFather</a>. Pass an empty string for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    public required string? ProviderToken { get; set; }

    /// <summary>Three-letter ISO 4217 currency code, see <a href="https://core.telegram.org/bots/payments#supported-currencies">more on currencies</a>. Pass “XTR” for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Currency { get; set; }

    /// <summary>Price breakdown, a list of components (e.g. product price, tax, discount, delivery cost, delivery tax, bonus, etc.). Must contain exactly one item for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<LabeledPrice> Prices { get; set; }

    /// <summary>The maximum accepted amount for tips in the <em>smallest units</em> of the currency (integer, <b>not</b> float/double). For example, for a maximum tip of <c>US$ 1.45</c> pass <c><see cref="MaxTipAmount">MaxTipAmount</see> = 145</c>. See the <em>exp</em> parameter in <a href="https://core.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies). Defaults to 0. Not supported for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    public int? MaxTipAmount { get; set; }

    /// <summary>A array of suggested amounts of tips in the <em>smallest units</em> of the currency (integer, <b>not</b> float/double). At most 4 suggested tip amounts can be specified. The suggested tip amounts must be positive, passed in a strictly increased order and must not exceed <see cref="MaxTipAmount">MaxTipAmount</see>.</summary>
    public IEnumerable<int>? SuggestedTipAmounts { get; set; }

    /// <summary>JSON-serialized data about the invoice, which will be shared with the payment provider. A detailed description of required fields should be provided by the payment provider.</summary>
    public string? ProviderData { get; set; }

    /// <summary>URL of the product photo for the invoice. Can be a photo of the goods or a marketing image for a service.</summary>
    public string? PhotoUrl { get; set; }

    /// <summary>Photo size in bytes</summary>
    public int? PhotoSize { get; set; }

    /// <summary>Photo width</summary>
    public int? PhotoWidth { get; set; }

    /// <summary>Photo height</summary>
    public int? PhotoHeight { get; set; }

    /// <summary>Pass <see langword="true"/> if you require the user's full name to complete the order. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    public bool NeedName { get; set; }

    /// <summary>Pass <see langword="true"/> if you require the user's phone number to complete the order. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    public bool NeedPhoneNumber { get; set; }

    /// <summary>Pass <see langword="true"/> if you require the user's email address to complete the order. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    public bool NeedEmail { get; set; }

    /// <summary>Pass <see langword="true"/> if you require the user's shipping address to complete the order. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    public bool NeedShippingAddress { get; set; }

    /// <summary>Pass <see langword="true"/> if the user's phone number should be sent to the provider. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    public bool SendPhoneNumberToProvider { get; set; }

    /// <summary>Pass <see langword="true"/> if the user's email address should be sent to the provider. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    public bool SendEmailToProvider { get; set; }

    /// <summary>Pass <see langword="true"/> if the final price depends on the shipping method. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    public bool IsFlexible { get; set; }

    /// <summary>Initializes an instance of <see cref="CreateInvoiceLinkRequest"/></summary>
    /// <param name="title">Product name, 1-32 characters</param>
    /// <param name="description">Product description, 1-255 characters</param>
    /// <param name="payload">Bot-defined invoice payload, 1-128 bytes. This will not be displayed to the user, use for your internal processes.</param>
    /// <param name="providerToken">Payment provider token, obtained via <a href="https://t.me/botfather">@BotFather</a>. Pass an empty string for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="currency">Three-letter ISO 4217 currency code, see <a href="https://core.telegram.org/bots/payments#supported-currencies">more on currencies</a>. Pass “XTR” for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="prices">Price breakdown, a list of components (e.g. product price, tax, discount, delivery cost, delivery tax, bonus, etc.). Must contain exactly one item for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public CreateInvoiceLinkRequest(string title, string description, string payload, string? providerToken, string currency, IEnumerable<LabeledPrice> prices) : this()
    {
        Title = title;
        Description = description;
        Payload = payload;
        ProviderToken = providerToken;
        Currency = currency;
        Prices = prices;
    }

    /// <summary>Instantiates a new <see cref="CreateInvoiceLinkRequest"/></summary>
    public CreateInvoiceLinkRequest() : base("createInvoiceLink") { }
}
