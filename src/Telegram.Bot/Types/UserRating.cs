// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes the rating of a user based on their Telegram Star spendings.</summary>
public partial class UserRating
{
    /// <summary>Current level of the user, indicating their reliability when purchasing digital goods and services. A higher level suggests a more trustworthy customer; a negative level is likely reason for concern.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Level { get; set; }

    /// <summary>Numerical value of the user's rating; the higher the rating, the better</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long Rating { get; set; }

    /// <summary>The rating value required to get the current level</summary>
    [JsonPropertyName("current_level_rating")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long CurrentLevelRating { get; set; }

    /// <summary><em>Optional</em>. The rating value required to get to the next level; omitted if the maximum level was reached</summary>
    [JsonPropertyName("next_level_rating")]
    public long? NextLevelRating { get; set; }
}
