namespace Telegram.Bot.Types;

/// <summary>
/// Represents a reaction added to a message along with the number of times it was added.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ReactionCount
{
    /// <summary>
    /// Type of the reaction
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public ReactionType Type { get; set; } = default!;

    /// <summary>
    /// Number of times the reaction was added
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int TotalCount { get; set; }
}
