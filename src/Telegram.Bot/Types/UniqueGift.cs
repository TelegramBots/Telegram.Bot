// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes a unique gift that was upgraded from a regular gift.</summary>
public partial class UniqueGift
{
    /// <summary>Identifier of the regular gift from which the gift was upgraded</summary>
    [JsonPropertyName("gift_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string GiftId { get; set; } = default!;

    /// <summary>Human-readable name of the regular gift from which this unique gift was upgraded</summary>
    [JsonPropertyName("base_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string BaseName { get; set; } = default!;

    /// <summary>Unique name of the gift. This name can be used in <c>https://t.me/nft/...</c> links and story areas</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Name { get; set; } = default!;

    /// <summary>Unique number of the upgraded gift among gifts upgraded from the same regular gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Number { get; set; }

    /// <summary>Model of the gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public UniqueGiftModel Model { get; set; } = default!;

    /// <summary>Symbol of the gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public UniqueGiftSymbol Symbol { get; set; } = default!;

    /// <summary>Backdrop of the gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public UniqueGiftBackdrop Backdrop { get; set; } = default!;

    /// <summary><em>Optional</em>. <see langword="true"/>, if the original regular gift was exclusively purchaseable by Telegram Premium subscribers</summary>
    [JsonPropertyName("is_premium")]
    public bool IsPremium { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the gift is assigned from the TON blockchain and can't be resold or transferred in Telegram</summary>
    [JsonPropertyName("is_from_blockchain")]
    public bool IsFromBlockchain { get; set; }

    /// <summary><em>Optional</em>. The color scheme that can be used by the gift's owner for the chat's name, replies to messages and link previews; for business account gifts and gifts that are currently on sale only</summary>
    public UniqueGiftColors? Colors { get; set; }

    /// <summary><em>Optional</em>. Information about the chat that published the gift</summary>
    [JsonPropertyName("publisher_chat")]
    public Chat? PublisherChat { get; set; }
}
