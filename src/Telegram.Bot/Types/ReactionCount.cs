// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Represents a reaction added to a message along with the number of times it was added.</summary>
public partial class ReactionCount
{
    /// <summary>Type of the reaction</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ReactionType Type { get; set; } = default!;

    /// <summary>Number of times the reaction was added</summary>
    [JsonPropertyName("total_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int TotalCount { get; set; }
}
