// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes a sticker on a TON wallet card.</summary>
public partial class TonWalletCardSticker
{
    /// <summary>Position of the sticker on the card</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public AreaPosition Position { get; set; } = default!;

    /// <summary>The NFT shown as the sticker</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public TonNft Nft { get; set; } = default!;
}
