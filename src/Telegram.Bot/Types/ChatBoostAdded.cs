namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about a user boosting a chat.
/// </summary>
public class ChatBoostAdded
{
    /// <summary>
    /// Number of boosts added by the user
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int BoostCount { get; set; }
}
