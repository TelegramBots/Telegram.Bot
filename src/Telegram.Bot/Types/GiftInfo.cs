// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about a regular gift that was sent or received.</summary>
public partial class GiftInfo
{
    /// <summary>Information about the gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Gift Gift { get; set; } = default!;

    /// <summary><em>Optional</em>. Unique identifier of the received gift for the bot; only present for gifts received on behalf of business accounts</summary>
    [JsonPropertyName("owned_gift_id")]
    public string? OwnedGiftId { get; set; }

    /// <summary><em>Optional</em>. Number of Telegram Stars that can be claimed by the receiver by converting the gift; omitted if conversion to Telegram Stars is impossible</summary>
    [JsonPropertyName("convert_star_count")]
    public long? ConvertStarCount { get; set; }

    /// <summary><em>Optional</em>. Number of Telegram Stars that were prepaid for the ability to upgrade the gift</summary>
    [JsonPropertyName("prepaid_upgrade_star_count")]
    public long? PrepaidUpgradeStarCount { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the gift's upgrade was purchased after the gift was sent</summary>
    [JsonPropertyName("is_upgrade_separate")]
    public bool IsUpgradeSeparate { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the gift can be upgraded to a unique gift</summary>
    [JsonPropertyName("can_be_upgraded")]
    public bool CanBeUpgraded { get; set; }

    /// <summary><em>Optional</em>. Text of the message that was added to the gift</summary>
    public string? Text { get; set; }

    /// <summary><em>Optional</em>. Special entities that appear in the text</summary>
    public MessageEntity[]? Entities { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the sender and gift text are shown only to the gift receiver; otherwise, everyone will be able to see them</summary>
    [JsonPropertyName("is_private")]
    public bool IsPrivate { get; set; }

    /// <summary><em>Optional</em>. Unique number reserved for this gift when upgraded. See the <em>number</em> field in <see cref="UniqueGift"/></summary>
    [JsonPropertyName("unique_gift_number")]
    public int? UniqueGiftNumber { get; set; }
}
