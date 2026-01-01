// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Transfers an owned unique gift to another user. Requires the <em>CanTransferAndUpgradeGifts</em> business bot right. Requires <em>CanTransferStars</em> business bot right if the transfer is paid.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class TransferGiftRequest() : RequestBase<bool>("transferGift"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>Unique identifier of the regular gift that should be transferred</summary>
    [JsonPropertyName("owned_gift_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string OwnedGiftId { get; set; }

    /// <summary>Unique identifier of the chat which will own the gift. The chat must be active in the last 24 hours.</summary>
    [JsonPropertyName("new_owner_chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long NewOwnerChatId { get; set; }

    /// <summary>The amount of Telegram Stars that will be paid for the transfer from the business account balance. If positive, then the <em>CanTransferStars</em> business bot right is required.</summary>
    [JsonPropertyName("star_count")]
    public long? StarCount { get; set; }
}
