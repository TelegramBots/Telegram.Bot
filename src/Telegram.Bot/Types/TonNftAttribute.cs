// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents an attribute of an NFT in the TON blockchain.</summary>
public partial class TonNftAttribute
{
    /// <summary>Trait type of the attribute</summary>
    [JsonPropertyName("trait_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string TraitType { get; set; } = default!;

    /// <summary>Value of the attribute</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Value { get; set; } = default!;
}
