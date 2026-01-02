// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about a unique gift that was sent or received.</summary>
public partial class UniqueGiftInfo
{
    /// <summary>Information about the gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public UniqueGift Gift { get; set; } = default!;

    /// <summary>Origin of the gift. Currently, either “upgrade” for gifts upgraded from regular gifts, “transfer” for gifts transferred from other users or channels, “resale” for gifts bought from other users, “GiftedUpgrade” for upgrades purchased after the gift was sent, or “offer” for gifts bought or sold through gift purchase offers</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Origin { get; set; } = default!;

    /// <summary><em>Optional</em>. For gifts bought from other users, the currency in which the payment for the gift was done. Currently, one of “XTR” for Telegram Stars or “TON” for toncoins.</summary>
    [JsonPropertyName("last_resale_currency")]
    public string? LastResaleCurrency { get; set; }

    /// <summary><em>Optional</em>. For gifts bought from other users, the price paid for the gift in either Telegram Stars or nanotoncoins</summary>
    [JsonPropertyName("last_resale_amount")]
    public long? LastResaleAmount { get; set; }

    /// <summary><em>Optional</em>. Unique identifier of the received gift for the bot; only present for gifts received on behalf of business accounts</summary>
    [JsonPropertyName("owned_gift_id")]
    public string? OwnedGiftId { get; set; }

    /// <summary><em>Optional</em>. Number of Telegram Stars that must be paid to transfer the gift; omitted if the bot cannot transfer the gift</summary>
    [JsonPropertyName("transfer_star_count")]
    public long? TransferStarCount { get; set; }

    /// <summary><em>Optional</em>. Point in time when the gift can be transferred. If it is in the past, then the gift can be transferred now</summary>
    [JsonPropertyName("next_transfer_date")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? NextTransferDate { get; set; }
}
