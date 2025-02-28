// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a boost removed from a chat.</summary>
public partial class ChatBoostRemoved
{
    /// <summary>Chat which was boosted</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat Chat { get; set; } = default!;

    /// <summary>Unique identifier of the boost</summary>
    [JsonPropertyName("boost_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string BoostId { get; set; } = default!;

    /// <summary>Point in time when the boost was removed</summary>
    [JsonPropertyName("remove_date")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime RemoveDate { get; set; }

    /// <summary>Source of the removed boost</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChatBoostSource Source { get; set; } = default!;
}
