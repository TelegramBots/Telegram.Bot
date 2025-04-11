// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes the backdrop of a unique gift.</summary>
public partial class UniqueGiftBackdrop
{
    /// <summary>Name of the backdrop</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Name { get; set; } = default!;

    /// <summary>Colors of the backdrop</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public UniqueGiftBackdropColors Colors { get; set; } = default!;

    /// <summary>The number of unique gifts that receive this backdrop for every 1000 gifts upgraded</summary>
    [JsonPropertyName("rarity_per_mille")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int RarityPerMille { get; set; }
}
