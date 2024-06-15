using Telegram.Bot.Serialization;

namespace Telegram.Bot.Types;

/// <summary>
/// This object contains information about a chat boost.
/// </summary>
public class ChatBoost
{
    /// <summary>
    /// Unique identifier of the boost
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string BoostId { get; set; } = default!;

    /// <summary>
    /// Point in time when the chat was boosted
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime AddDate { get; set; }

    /// <summary>
    /// Point in time when the boost will automatically expire, unless the booster's Telegram Premium subscription is prolonged
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime ExpirationDate { get; set; }

    /// <summary>
    /// Source of the added boost
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChatBoostSource Source  { get; set; } = default!;
}
