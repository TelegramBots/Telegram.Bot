// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes a TON wallet card, which is shown on the profile page of a user to everyone.</summary>
public partial class TonWalletCard
{
    /// <summary>Address of the wallet</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Address { get; set; } = default!;

    /// <summary>Balance of the wallet, in the <em>smallest units</em> of the cryptocurrency, as a decimal number</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Amount { get; set; } = default!;

    /// <summary>Stickers placed on the card</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public TonWalletCardSticker[] Stickers { get; set; } = default!;

    /// <summary><em>Optional</em>. Address of the NFT of one of the stickers, whose colors must be used for the card background; omitted if the default background must be used</summary>
    [JsonPropertyName("background_nft_address")]
    public string? BackgroundNftAddress { get; set; }
}
