namespace Telegram.Bot.Requests;

/// <summary>Use this method to send invoices.<para>Returns: The sent <see cref="Message"/> is returned.</para></summary>
public partial class SendInvoiceRequest : RequestBase<Message>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

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

    /// <summary>Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</summary>
    public int? MessageThreadId { get; set; }

    /// <summary>The maximum accepted amount for tips in the <em>smallest units</em> of the currency (integer, <b>not</b> float/double). For example, for a maximum tip of <c>US$ 1.45</c> pass <c><see cref="MaxTipAmount">MaxTipAmount</see> = 145</c>. See the <em>exp</em> parameter in <a href="https://core.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies). Defaults to 0. Not supported for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    public int? MaxTipAmount { get; set; }

    /// <summary>A array of suggested amounts of tips in the <em>smallest units</em> of the currency (integer, <b>not</b> float/double). At most 4 suggested tip amounts can be specified. The suggested tip amounts must be positive, passed in a strictly increased order and must not exceed <see cref="MaxTipAmount">MaxTipAmount</see>.</summary>
    public IEnumerable<int>? SuggestedTipAmounts { get; set; }

    /// <summary>Unique deep-linking parameter. If left empty, <b>forwarded copies</b> of the sent message will have a <em>Pay</em> button, allowing multiple users to pay directly from the forwarded message, using the same invoice. If non-empty, forwarded copies of the sent message will have a <em>URL</em> button with a deep link to the bot (instead of a <em>Pay</em> button), with the value used as the start parameter</summary>
    public string? StartParameter { get; set; }

    /// <summary>JSON-serialized data about the invoice, which will be shared with the payment provider. A detailed description of required fields should be provided by the payment provider.</summary>
    public string? ProviderData { get; set; }

    /// <summary>URL of the product photo for the invoice. Can be a photo of the goods or a marketing image for a service. People like it better when they see what they are paying for.</summary>
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

    /// <summary>Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</summary>
    public bool DisableNotification { get; set; }

    /// <summary>Protects the contents of the sent message from forwarding and saving</summary>
    public bool ProtectContent { get; set; }

    /// <summary>Unique identifier of the message effect to be added to the message; for private chats only</summary>
    public string? MessageEffectId { get; set; }

    /// <summary>Description of the message to reply to</summary>
    public ReplyParameters? ReplyParameters { get; set; }

    /// <summary>An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>. If empty, one 'Pay <c>total price</c>' button will be shown. If not empty, the first button must be a Pay button.</summary>
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>Initializes an instance of <see cref="SendInvoiceRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="title">Product name, 1-32 characters</param>
    /// <param name="description">Product description, 1-255 characters</param>
    /// <param name="payload">Bot-defined invoice payload, 1-128 bytes. This will not be displayed to the user, use for your internal processes.</param>
    /// <param name="providerToken">Payment provider token, obtained via <a href="https://t.me/botfather">@BotFather</a>. Pass an empty string for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="currency">Three-letter ISO 4217 currency code, see <a href="https://core.telegram.org/bots/payments#supported-currencies">more on currencies</a>. Pass “XTR” for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="prices">Price breakdown, a list of components (e.g. product price, tax, discount, delivery cost, delivery tax, bonus, etc.). Must contain exactly one item for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SendInvoiceRequest(ChatId chatId, string title, string description, string payload, string? providerToken, string currency, IEnumerable<LabeledPrice> prices) : this()
    {
        ChatId = chatId;
        Title = title;
        Description = description;
        Payload = payload;
        ProviderToken = providerToken;
        Currency = currency;
        Prices = prices;
    }

    /// <summary>Instantiates a new <see cref="SendInvoiceRequest"/></summary>
    public SendInvoiceRequest() : base("sendInvoice") { }
}
