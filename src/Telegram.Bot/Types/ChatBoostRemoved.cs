using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a boost removed from a chat.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ChatBoostRemoved
{
    /// <summary>
    /// Chat which was boosted
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public Chat Chat { get; set; } = default!;

    /// <summary>
    /// Unique identifier of the boost
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string BoostId { get; set; } = default!;

    /// <summary>
    /// Point in time when the boost was removed
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime RemoveDate { get; set; }

    /// <summary>
    /// Source of the removed boost
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public ChatBoostSource Source { get; set; } = default!;
}
