// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Verifies a user <a href="https://telegram.org/verify#third-party-verification">on behalf of the organization</a> which is represented by the bot.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class VerifyUserRequest() : RequestBase<bool>("verifyUser"), IUserTargetable
{
    /// <summary>Unique identifier of the target user</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Custom description for the verification; 0-70 characters. Must be empty if the organization isn't allowed to provide a custom verification description.</summary>
    [JsonPropertyName("custom_description")]
    public string? CustomDescription { get; set; }

    /// <summary>Mode for parsing entities in the custom description. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details. Only <see cref="MessageEntityType.TextLink">TextLink</see> entities are supported.</summary>
    [JsonPropertyName("custom_description_parse_mode")]
    public ParseMode CustomDescriptionParseMode { get; set; }

    /// <summary>A list of special entities that appear in the custom description, which can be specified instead of <see cref="CustomDescriptionParseMode">CustomDescriptionParseMode</see>. Only “TextLink” entities are supported.</summary>
    [JsonPropertyName("custom_description_entities")]
    public IEnumerable<MessageEntity>? CustomDescriptionEntities { get; set; }
}
