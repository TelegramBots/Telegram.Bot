using Telegram.Bot.Serialization;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a boost removed from a chat.
/// </summary>
public class ChatBoostRemoved
{
    /// <summary>
    /// Chat which was boosted
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat Chat { get; set; } = default!;

    /// <summary>
    /// Unique identifier of the boost
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string BoostId { get; set; } = default!;

    /// <summary>
    /// Point in time when the boost was removed
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime RemoveDate { get; set; }

    /// <summary>
    /// Source of the removed boost
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChatBoostSource Source { get; set; } = default!;
}
