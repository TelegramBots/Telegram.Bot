namespace Telegram.Bot.Types;

/// <summary>
/// Represents a reaction added to a message along with the number of times it was added.
/// </summary>
public class ReactionCount
{
    /// <summary>
    /// Type of the reaction
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ReactionType Type { get; set; } = default!;

    /// <summary>
    /// Number of times the reaction was added
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int TotalCount { get; set; }
}
