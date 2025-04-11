// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes the symbol shown on the pattern of a unique gift.</summary>
public partial class UniqueGiftSymbol
{
    /// <summary>Name of the symbol</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Name { get; set; } = default!;

    /// <summary>The sticker that represents the unique gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Sticker Sticker { get; set; } = default!;

    /// <summary>The number of unique gifts that receive this model for every 1000 gifts upgraded</summary>
    [JsonPropertyName("rarity_per_mille")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int RarityPerMille { get; set; }
}
