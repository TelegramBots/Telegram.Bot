// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents one row of the high scores table for a game.</summary>
public partial class GameHighScore
{
    /// <summary>Position in high score table for the game</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Position { get; set; }

    /// <summary>User</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User User { get; set; } = default!;

    /// <summary>Score</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Score { get; set; }
}
