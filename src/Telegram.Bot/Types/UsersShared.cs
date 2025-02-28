// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object contains information about the users whose identifiers were shared with the bot using a <see cref="KeyboardButtonRequestUsers"/> button.</summary>
public partial class UsersShared
{
    /// <summary>Identifier of the request</summary>
    [JsonPropertyName("request_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int RequestId { get; set; }

    /// <summary>Information about users shared with the bot.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public SharedUser[] Users { get; set; } = default!;
}
