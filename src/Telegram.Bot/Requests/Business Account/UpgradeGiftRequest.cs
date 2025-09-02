// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Upgrades a given regular gift to a unique gift. Requires the <em>CanTransferAndUpgradeGifts</em> business bot right. Additionally requires the <em>CanTransferStars</em> business bot right if the upgrade is paid.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class UpgradeGiftRequest() : RequestBase<bool>("upgradeGift"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>Unique identifier of the regular gift that should be upgraded to a unique one</summary>
    [JsonPropertyName("owned_gift_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string OwnedGiftId { get; set; }

    /// <summary>Pass <see langword="true"/> to keep the original gift text, sender and receiver in the upgraded gift</summary>
    [JsonPropertyName("keep_original_details")]
    public bool KeepOriginalDetails { get; set; }

    /// <summary>The amount of Telegram Stars that will be paid for the upgrade from the business account balance. If <c>gift.PrepaidUpgradeStarCount &gt; 0</c>, then pass 0, otherwise, the <em>CanTransferStars</em> business bot right is required and <c>gift.UpgradeStarCount</c> must be passed.</summary>
    [JsonPropertyName("star_count")]
    public long? StarCount { get; set; }
}
