// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about a payment refund for a suggested post.</summary>
public partial class SuggestedPostRefunded
{
    /// <summary><em>Optional</em>. Message containing the suggested post. Note that the <see cref="Message"/> object in this field will not contain the <em>ReplyToMessage</em> field even if it itself is a reply.</summary>
    [JsonPropertyName("suggested_post_message")]
    public Message? SuggestedPostMessage { get; set; }

    /// <summary>Reason for the refund. Currently, one of <see cref="SuggestedPostRefundedReason.PostDeleted">PostDeleted</see> if the post was deleted within 24 hours of being posted or removed from scheduled messages without being posted, or <see cref="SuggestedPostRefundedReason.PaymentRefunded">PaymentRefunded</see> if the payer refunded their payment.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public SuggestedPostRefundedReason Reason { get; set; }
}
