namespace Telegram.Bot.Types;

/// <summary>This object represents a phone contact.</summary>
public partial class Contact
{
    /// <summary>Contact's phone number</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string PhoneNumber { get; set; } = default!;

    /// <summary>Contact's first name</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string FirstName { get; set; } = default!;

    /// <summary><em>Optional</em>. Contact's last name</summary>
    public string? LastName { get; set; }

    /// <summary><em>Optional</em>. Contact's user identifier in Telegram.</summary>
    public long? UserId { get; set; }

    /// <summary><em>Optional</em>. Additional data about the contact in the form of a <a href="https://en.wikipedia.org/wiki/VCard">vCard</a></summary>
    public string? Vcard { get; set; }
}
