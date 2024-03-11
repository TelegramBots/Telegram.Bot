using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Types;

/// <summary>
/// This object contains information about a chat boost.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ChatBoost
{
    /// <summary>
    /// Unique identifier of the boost
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string BoostId { get; set; } = default!;

    /// <summary>
    /// Point in time when the chat was boosted
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime AddDate { get; set; }

    /// <summary>
    /// Point in time when the boost will automatically expire, unless the booster's Telegram Premium subscription is prolonged
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime ExpirationDate { get; set; }

    /// <summary>
    /// Source of the added boost
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public ChatBoostSource Source  { get; set; } = default!;
}
