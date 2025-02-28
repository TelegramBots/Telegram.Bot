// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a service message about a user boosting a chat.</summary>
public partial class ChatBoostAdded
{
    /// <summary>Number of boosts added by the user</summary>
    [JsonPropertyName("boost_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int BoostCount { get; set; }

    /// <summary>Implicit conversion to int (BoostCount)</summary>
    public static implicit operator int(ChatBoostAdded self) => self.BoostCount;
    /// <summary>Implicit conversion from int (BoostCount)</summary>
    public static implicit operator ChatBoostAdded(int boostCount) => new() { BoostCount = boostCount };
}
