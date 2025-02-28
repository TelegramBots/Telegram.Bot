// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a phone contact.</summary>
public partial class Contact
{
    /// <summary>Contact's phone number</summary>
    [JsonPropertyName("phone_number")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string PhoneNumber { get; set; } = default!;

    /// <summary>Contact's first name</summary>
    [JsonPropertyName("first_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string FirstName { get; set; } = default!;

    /// <summary><em>Optional</em>. Contact's last name</summary>
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    /// <summary><em>Optional</em>. Contact's user identifier in Telegram.</summary>
    [JsonPropertyName("user_id")]
    public long? UserId { get; set; }

    /// <summary><em>Optional</em>. Additional data about the contact in the form of a <a href="https://en.wikipedia.org/wiki/VCard">vCard</a></summary>
    public string? Vcard { get; set; }
}
