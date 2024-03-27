namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a phone contact.
/// </summary>
public class Contact
{
    /// <summary>
    /// Contact's phone number
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string PhoneNumber { get; set; } = default!;

    /// <summary>
    /// Contact's first name
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Optional. Contact's last name
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LastName { get; set; }

    /// <summary>
    /// Optional. Contact's user identifier in Telegram
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? UserId { get; set; }

    /// <summary>
    /// Optional. Additional data about the contact in the form of a vCard
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Vcard { get; set; }
}
