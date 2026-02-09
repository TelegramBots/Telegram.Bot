// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents the audios displayed on a user's profile.</summary>
public partial class UserProfileAudios
{
    /// <summary>Total number of profile audios for the target user</summary>
    [JsonPropertyName("total_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int TotalCount { get; set; }

    /// <summary>Requested profile audios</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Audio[] Audios { get; set; } = default!;
}
