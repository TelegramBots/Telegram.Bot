// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes the paid media added to a message.</summary>
public partial class PaidMediaInfo
{
    /// <summary>The number of Telegram Stars that must be paid to buy access to the media</summary>
    [JsonPropertyName("star_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long StarCount { get; set; }

    /// <summary>Information about the paid media</summary>
    [JsonPropertyName("paid_media")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public PaidMedia[] PaidMedia { get; set; } = default!;
}
