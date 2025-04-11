// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Contains the list of gifts received and owned by a user or a chat.</summary>
public partial class OwnedGifts
{
    /// <summary>The total number of gifts owned by the user or the chat</summary>
    [JsonPropertyName("total_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int TotalCount { get; set; }

    /// <summary>The list of gifts</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public OwnedGift[] Gifts { get; set; } = default!;

    /// <summary><em>Optional</em>. Offset for the next request. If empty, then there are no more results</summary>
    [JsonPropertyName("next_offset")]
    public string? NextOffset { get; set; }
}
