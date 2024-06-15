namespace Telegram.Bot.Types;

/// <summary>
/// This object represent a user's profile pictures.
/// </summary>
public class UserProfilePhotos
{
    /// <summary>
    /// Total number of profile pictures the target user has
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int TotalCount { get; set; }

    /// <summary>
    /// Requested profile pictures (in up to 4 sizes each)
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public PhotoSize[][] Photos { get; set; } = default!;
}
