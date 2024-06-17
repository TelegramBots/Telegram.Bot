using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Serialization;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// This object represents the content of a message to be sent as a result of an
/// <see cref="InlineQuery">inline query</see>.
/// </summary>
[CustomJsonPolymorphic]
[CustomJsonDerivedType(typeof(InputContactMessageContent))]
[CustomJsonDerivedType(typeof(InputInvoiceMessageContent))]
[CustomJsonDerivedType(typeof(InputLocationMessageContent))]
[CustomJsonDerivedType(typeof(InputTextMessageContent))]
[CustomJsonDerivedType(typeof(InputVenueMessageContent))]
public abstract class InputMessageContent;

/// <summary>
/// Represents the content of a text message to be sent as the result of an
/// <see cref="InlineQuery">inline query</see>.
/// </summary>
public class InputTextMessageContent : InputMessageContent
{
    /// <summary>
    /// Text of the message to be sent, 1-4096 characters
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string MessageText { get; init; }

    /// <summary>
    /// Optional. Mode for
    /// <a href="https://core.telegram.org/bots/api#formatting-options">parsing entities</a> in the message
    /// text. See formatting options for more details.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ParseMode? ParseMode { get; set; }

    /// <summary>
    /// Optional. List of special entities that appear in message text, which can be specified
    /// instead of <see cref="ParseMode"/>
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MessageEntity[]? Entities { get; set; } // ToDo: add test

    /// <summary>
    /// Optional. Link preview generation options for the message
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public LinkPreviewOptions? LinkPreviewOptions { get; set; }

    /// <summary>
    /// Disables link previews for links in this message
    /// </summary>
    [Obsolete($"This property is deprecated, use {nameof(LinkPreviewOptions)} instead")]
    [JsonIgnore]
    public bool? DisableWebPagePreview
    {
        get => LinkPreviewOptions?.IsDisabled;
        set
        {
            LinkPreviewOptions ??= new();
            LinkPreviewOptions.IsDisabled = value;
        }
    }

    /// <summary>
    /// Initializes a new input text message content
    /// </summary>
    /// <param name="messageText">The text of the message</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public InputTextMessageContent(string messageText)
    {
        MessageText = messageText;
    }

    /// <summary>
    /// Initializes a new input text message content
    /// </summary>
    public InputTextMessageContent()
    { }
}

/// <summary>
/// Represents the content of a location message to be sent as the result of an
/// <see cref="InlineQuery">inline query</see>.
/// </summary>
public class InputLocationMessageContent : InputMessageContent
{
    /// <summary>
    /// Latitude of the location in degrees
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Latitude { get; init; }

    /// <summary>
    /// Longitude of the location in degrees
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Longitude { get; init; }

    /// <summary>
    /// Optional. The radius of uncertainty for the location, measured in meters; 0-1500
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? HorizontalAccuracy { get; set; }

    /// <summary>
    /// Optional. Period in seconds for which the location can be updated, should be between 60 and 86400.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? LivePeriod { get; set; }

    /// <summary>
    /// Optional. The direction in which user is moving, in degrees; 1-360. For active live locations only.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Heading { get; set; }

    /// <summary>
    /// Optional. Maximum distance for proximity alerts about approaching another chat member,
    /// in meters. For sent live locations only.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ProximityAlertRadius { get; set; }

    /// <summary>
    /// Initializes a new input location message content
    /// </summary>
    /// <param name="latitude">The latitude of the location</param>
    /// <param name="longitude">The longitude of the location</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public InputLocationMessageContent(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    /// <summary>
    /// Initializes a new input location message content
    /// </summary>
    public InputLocationMessageContent()
    { }
}

/// <summary>
/// Represents the content of a <see cref="Venue"/> message to be sent as the result of an
/// <see cref="InlineQuery">inline query</see>.
/// </summary>
public class InputVenueMessageContent : InputMessageContent
{
    /// <summary>
    /// Latitude of the venue in degrees
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Latitude { get; init; }

    /// <summary>
    /// Longitude of the venue in degrees
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Longitude { get; init; }

    /// <summary>
    /// Name of the venue
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; init; }

    /// <summary>
    /// Address of the venue
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Address { get; init; }

    /// <summary>
    /// Optional. Foursquare identifier of the venue, if known
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FoursquareId { get; set; }

    /// <summary>
    /// Optional. Foursquare type of the venue. (For example, “arts_entertainment/default”,
    /// “arts_entertainment/aquarium” or “food/icecream”.)
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FoursquareType { get; set; }

    /// <summary>
    /// Google Places identifier of the venue
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GooglePlaceId { get; set; }

    /// <summary>
    /// Google Places type of the venue.
    /// <a href="https://developers.google.com/places/web-service/supported_types"/>
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GooglePlaceType { get; set; }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    /// <param name="title">The name of the venue</param>
    /// <param name="address">The address of the venue</param>
    /// <param name="latitude">The latitude of the venue</param>
    /// <param name="longitude">The longitude of the venue</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public InputVenueMessageContent(string title, string address, double latitude, double longitude)
    {
        Title = title;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    public InputVenueMessageContent()
    { }
}

/// <summary>
/// Represents the content of a contact message to be sent as the result of an <see cref="InlineQuery">inline query</see>.
/// </summary>
public class InputContactMessageContent : InputMessageContent
{
    /// <summary>
    /// Contact's phone number
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string PhoneNumber { get; init; }

    /// <summary>
    /// Contact's first name
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string FirstName { get; init; }

    /// <summary>
    /// Optional. Contact's last name
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LastName { get; set; }

    /// <summary>
    /// Optional. Additional data about the contact in the form of a vCard, 0-2048 bytes
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Vcard { get; set; }

    /// <summary>
    /// Initializes a new input contact message content
    /// </summary>
    /// <param name="phoneNumber">The phone number of the contact</param>
    /// <param name="firstName">The first name of the contact</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public InputContactMessageContent(string phoneNumber, string firstName)
    {
        PhoneNumber = phoneNumber;
        FirstName = firstName;
    }

    /// <summary>
    /// Initializes a new input contact message content
    /// </summary>
    public InputContactMessageContent()
    { }
}

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
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ProviderToken { get; set; }

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
