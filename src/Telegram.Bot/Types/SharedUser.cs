// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object contains information about a user that was shared with the bot using a <see cref="KeyboardButtonRequestUsers"/> button.</summary>
public partial class SharedUser
{
    /// <summary>Identifier of the shared user. The bot may not have access to the user and could be unable to use this identifier, unless the user is already known to the bot by some other means.</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long UserId { get; set; }

    /// <summary><em>Optional</em>. First name of the user, if the name was requested by the bot</summary>
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    /// <summary><em>Optional</em>. Last name of the user, if the name was requested by the bot</summary>
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    /// <summary><em>Optional</em>. Username of the user, if the username was requested by the bot</summary>
    public string? Username { get; set; }

    /// <summary><em>Optional</em>. Available sizes of the chat photo, if the photo was requested by the bot</summary>
    public PhotoSize[]? Photo { get; set; }
}
