// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represent a user's profile pictures.</summary>
public partial class UserProfilePhotos
{
    /// <summary>Total number of profile pictures the target user has</summary>
    [JsonPropertyName("total_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int TotalCount { get; set; }

    /// <summary>Requested profile pictures (in up to 4 sizes each)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public PhotoSize[][] Photos { get; set; } = default!;
}
