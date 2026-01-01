// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes a gift received and owned by a user or a chat. Currently, it can be one of<br/><see cref="OwnedGiftRegular"/>, <see cref="OwnedGiftUnique"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<OwnedGift>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(OwnedGiftRegular), "regular")]
[CustomJsonDerivedType(typeof(OwnedGiftUnique), "unique")]
public abstract partial class OwnedGift
{
    /// <summary>Type of the gift, always <see cref="OwnedGift"/></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract OwnedGiftType Type { get; }

    /// <summary><em>Optional</em>. </summary>
    [JsonPropertyName("owned_gift_id")]
    public string? OwnedGiftId { get; set; }

    /// <summary><em>Optional</em>. Sender of the gift if it is a known user</summary>
    [JsonPropertyName("sender_user")]
    public User? SenderUser { get; set; }

    /// <summary>Date the gift was sent</summary>
    [JsonPropertyName("send_date")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime SendDate { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the gift is displayed on the account's profile page; for gifts received on behalf of business accounts only</summary>
    [JsonPropertyName("is_saved")]
    public bool IsSaved { get; set; }
}

/// <summary>Describes a regular gift owned by a user or a chat.</summary>
public partial class OwnedGiftRegular : OwnedGift
{
    /// <summary>Type of the gift, always <see cref="OwnedGiftType.Regular"/></summary>
    public override OwnedGiftType Type => OwnedGiftType.Regular;

    /// <summary>Information about the regular gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Gift Gift { get; set; } = default!;

    /// <summary><em>Optional</em>. Text of the message that was added to the gift</summary>
    public string? Text { get; set; }

    /// <summary><em>Optional</em>. Special entities that appear in the text</summary>
    public MessageEntity[]? Entities { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the sender and gift text are shown only to the gift receiver; otherwise, everyone will be able to see them</summary>
    [JsonPropertyName("is_private")]
    public bool IsPrivate { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the gift can be upgraded to a unique gift; for gifts received on behalf of business accounts only</summary>
    [JsonPropertyName("can_be_upgraded")]
    public bool CanBeUpgraded { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the gift was refunded and isn't available anymore</summary>
    [JsonPropertyName("was_refunded")]
    public bool WasRefunded { get; set; }

    /// <summary><em>Optional</em>. Number of Telegram Stars that can be claimed by the receiver instead of the gift; omitted if the gift cannot be converted to Telegram Stars; for gifts received on behalf of business accounts only</summary>
    [JsonPropertyName("convert_star_count")]
    public long? ConvertStarCount { get; set; }

    /// <summary><em>Optional</em>. Number of Telegram Stars that were paid for the ability to upgrade the gift</summary>
    [JsonPropertyName("prepaid_upgrade_star_count")]
    public long? PrepaidUpgradeStarCount { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the gift's upgrade was purchased after the gift was sent; for gifts received on behalf of business accounts only</summary>
    [JsonPropertyName("is_upgrade_separate")]
    public bool IsUpgradeSeparate { get; set; }

    /// <summary><em>Optional</em>. Unique number reserved for this gift when upgraded. See the <em>number</em> field in <see cref="UniqueGift"/></summary>
    [JsonPropertyName("unique_gift_number")]
    public int? UniqueGiftNumber { get; set; }
}

/// <summary>Describes a unique gift received and owned by a user or a chat.</summary>
public partial class OwnedGiftUnique : OwnedGift
{
    /// <summary>Type of the gift, always <see cref="OwnedGiftType.Unique"/></summary>
    public override OwnedGiftType Type => OwnedGiftType.Unique;

    /// <summary>Information about the unique gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public UniqueGift Gift { get; set; } = default!;

    /// <summary><em>Optional</em>. <see langword="true"/>, if the gift can be transferred to another owner; for gifts received on behalf of business accounts only</summary>
    [JsonPropertyName("can_be_transferred")]
    public bool CanBeTransferred { get; set; }

    /// <summary><em>Optional</em>. Number of Telegram Stars that must be paid to transfer the gift; omitted if the bot cannot transfer the gift</summary>
    [JsonPropertyName("transfer_star_count")]
    public long? TransferStarCount { get; set; }

    /// <summary><em>Optional</em>. Point in time when the gift can be transferred. If it is in the past, then the gift can be transferred now</summary>
    [JsonPropertyName("next_transfer_date")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? NextTransferDate { get; set; }
}
