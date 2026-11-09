// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents an NFT in the TON blockchain.</summary>
public partial class TonNft
{
    /// <summary>Address of the NFT</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Address { get; set; } = default!;

    /// <summary><em>Optional</em>. Address of the collection to which the NFT belongs; omitted if unknown</summary>
    [JsonPropertyName("collection_address")]
    public string? CollectionAddress { get; set; }

    /// <summary>Index of the NFT among all NFTs from the collection as a decimal number</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Index { get; set; } = default!;

    /// <summary>Name of the NFT</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Name { get; set; } = default!;

    /// <summary>Description of the NFT</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Description { get; set; } = default!;

    /// <summary><em>Optional</em>. Attributes of the NFT</summary>
    public TonNftAttribute[]? Attributes { get; set; }

    /// <summary><em>Optional</em>. The unique gift corresponding to the NFT</summary>
    public UniqueGift? Gift { get; set; }
}
