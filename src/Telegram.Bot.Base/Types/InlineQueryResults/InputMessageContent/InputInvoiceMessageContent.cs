using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types.Payments;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents the content of an invoice message to be sent as the result of an
/// <see cref="InlineQuery">inline query</see>.
/// </summary>
public class InputInvoiceMessageContent : InputMessageContent
{
    /// <summary>
    /// Product name, 1-32 characters
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; init; }

    /// <summary>
    /// Product description, 1-255 characters
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Description { get; init; }

    /// <summary>
    /// Bot-defined invoice payload, 1-128 bytes. This will not be displayed to the user,
    /// use for your internal processes.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Payload { get; init; }

    /// <summary>
    /// Payment provider token, obtained via <a href="https://t.me/botfather">@BotFather</a>
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string ProviderToken { get; init; }

    /// <summary>
    /// Three-letter ISO 4217 currency code, see
    /// <a href="https://core.telegram.org/bots/payments#supported-currencies">more on currencies</a>
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Currency { get; init; }

    /// <summary>
    /// Price breakdown, a list of components (e.g. product price, tax, discount, delivery cost,
    /// delivery tax, bonus, etc.)
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<LabeledPrice> Prices { get; init; }

    /// <summary>
    /// Optional. The maximum accepted amount for tips in the smallest units of the currency
    /// (integer, not float/double). For example, for a maximum tip of US$ 1.45 pass
    /// max_tip_amount = 145. See the exp parameter in
    /// <a href="https://core.telegram.org/bots/payments/currencies.json">currencies.json</a>,
    /// it shows the number of digits past the decimal point for each currency (2 for the
    /// majority of currencies). Defaults to 0
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MaxTipAmount { get; set; }

    /// <summary>
    /// Optional. An array of suggested amounts of tip in the smallest units of the currency
    /// (integer, not float/double). At most 4 suggested tip amounts can be specified. The
    /// suggested tip amounts must be positive, passed in a strictly increased order and
    /// must not exceed <see cref="MaxTipAmount"/>.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int[]? SuggestedTipAmounts { get; set; }

    /// <summary>
    /// Optional. An object for data about the invoice, which will be shared with
    /// the payment provider. A detailed description of the required fields should be provided by
    /// the payment provider.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ProviderData { get; set; }

    /// <summary>
    /// Optional. URL of the product photo for the invoice. Can be a photo of the goods or a
    /// marketing image for a service. People like it better when they see what they are paying for.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PhotoUrl { get; set; }

    /// <summary>
    /// Optional. Photo size
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PhotoSize { get; set; }

    /// <summary>
    /// Optional. Photo width
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PhotoWidth { get; set; }

    /// <summary>
    /// Optional. Photo height
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PhotoHeight { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true"/>, if you require the user's full name to complete the order
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? NeedName { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true"/>, if you require the user's phone number to complete the order
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? NeedPhoneNumber { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true"/>, if you require the user's email address to complete the order
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? NeedEmail { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true"/>, if you require the user's shipping address to complete the order
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? NeedShippingAddress { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true"/>, if user's phone number should be sent to provider
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? SendPhoneNumberToProvider { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true"/>, if user's email address should be sent to provider
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? SendEmailToProvider { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true"/>, if the final price depends on the shipping method
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsFlexible { get; set; }

    /// <summary>
    /// Initializes with title, description, payload, providerToken, currency and an array of
    /// <see cref="LabeledPrice"/>
    /// </summary>
    /// <param name="title">Product name, 1-32 characters</param>
    /// <param name="description">Product description, 1-255 characters</param>
    /// <param name="payload">Bot-defined invoice payload, 1-128 bytes</param>
    /// <param name="providerToken">Payments provider token, obtained via BotFather</param>
    /// <param name="currency">Three-letter ISO 4217 currency code</param>
    /// <param name="prices">
    /// Price breakdown, a list of components (e.g. product price, tax, discount, delivery cost,
    /// delivery tax, bonus, etc.)
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public InputInvoiceMessageContent(
        string title,
        string description,
        string payload,
        string providerToken,
        string currency,
        IEnumerable<LabeledPrice> prices)
    {
        Title = title;
        Description = description;
        Payload = payload;
        ProviderToken = providerToken;
        Currency = currency;
        Prices = prices;
    }

    /// <summary>
    /// Initializes a new input message content
    /// </summary>
    public InputInvoiceMessageContent()
    { }
}
