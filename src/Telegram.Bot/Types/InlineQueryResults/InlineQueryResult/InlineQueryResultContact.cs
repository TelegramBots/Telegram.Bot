

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents a contact with a phone number. By default, this contact will be sent by the user.
/// Alternatively, you can use <see cref="InlineQueryResultContact.InputMessageContent"/> to send
/// a message with the specified content instead of the contact.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class InlineQueryResultContact : InlineQueryResult
{
    /// <summary>
    /// Type of the result, must be contact
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public override InlineQueryResultType Type => InlineQueryResultType.Contact;

    /// <summary>
    /// Contact's phone number
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string PhoneNumber { get; }

    /// <summary>
    /// Contact's first name
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string FirstName { get; }

    /// <summary>
    /// Optional. Contact's last name
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? LastName { get; set; }

    /// <summary>
    /// Optional. Additional data about the contact in the form of a vCard, 0-2048 bytes
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Vcard { get; set; }

    /// <inheritdoc cref="Documentation.InputMessageContent" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <inheritdoc cref="Documentation.ThumbnailUrl" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? ThumbnailUrl { get; set; }

    /// <inheritdoc cref="Documentation.ThumbnailWidth" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? ThumbnailWidth { get; set; }

    /// <inheritdoc cref="Documentation.ThumbnailHeight" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? ThumbnailHeight { get; set; }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    /// <param name="id">Unique identifier of this result</param>
    /// <param name="phoneNumber">Contact's phone number</param>
    /// <param name="firstName">Contact's first name</param>
    public InlineQueryResultContact(string id, string phoneNumber, string firstName)
        : base(id)
    {
        PhoneNumber = phoneNumber;
        FirstName = firstName;
    }
}
