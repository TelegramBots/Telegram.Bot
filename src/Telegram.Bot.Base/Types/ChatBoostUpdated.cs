namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a boost added to a chat or changed.
/// </summary>
public class ChatBoostUpdated
{
    /// <summary>
    /// Chat which was boosted
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat Chat { get; set; } = default!;

    /// <summary>
    /// Information about the chat boost
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChatBoost Boost { get; set; } = default!;
}
