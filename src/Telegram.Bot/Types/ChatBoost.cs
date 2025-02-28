// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object contains information about a chat boost.</summary>
public partial class ChatBoost
{
    /// <summary>Unique identifier of the boost</summary>
    [JsonPropertyName("boost_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string BoostId { get; set; } = default!;

    /// <summary>Point in time when the chat was boosted</summary>
    [JsonPropertyName("add_date")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime AddDate { get; set; }

    /// <summary>Point in time when the boost will automatically expire, unless the booster's Telegram Premium subscription is prolonged</summary>
    [JsonPropertyName("expiration_date")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime ExpirationDate { get; set; }

    /// <summary>Source of the added boost</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChatBoostSource Source { get; set; } = default!;
}
