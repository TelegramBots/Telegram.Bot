// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes the birthdate of a user.</summary>
public partial class Birthdate
{
    /// <summary>Day of the user's birth; 1-31</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Day { get; set; }

    /// <summary>Month of the user's birth; 1-12</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Month { get; set; }

    /// <summary><em>Optional</em>. Year of the user's birth</summary>
    public int? Year { get; set; }
}
