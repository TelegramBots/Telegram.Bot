// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a gift that can be sent by the bot.</summary>
public partial class Gift
{
    /// <summary>Unique identifier of the gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Id { get; set; } = default!;

    /// <summary>The sticker that represents the gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Sticker Sticker { get; set; } = default!;

    /// <summary>The number of Telegram Stars that must be paid to send the sticker</summary>
    [JsonPropertyName("star_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int StarCount { get; set; }

    /// <summary><em>Optional</em>. The number of Telegram Stars that must be paid to upgrade the gift to a unique one</summary>
    [JsonPropertyName("upgrade_star_count")]
    public int? UpgradeStarCount { get; set; }

    /// <summary><em>Optional</em>. The total number of the gifts of this type that can be sent; for limited gifts only</summary>
    [JsonPropertyName("total_count")]
    public int? TotalCount { get; set; }

    /// <summary><em>Optional</em>. The number of remaining gifts of this type that can be sent; for limited gifts only</summary>
    [JsonPropertyName("remaining_count")]
    public int? RemainingCount { get; set; }
}
