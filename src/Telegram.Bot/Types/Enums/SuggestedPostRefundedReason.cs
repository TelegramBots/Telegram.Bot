// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary><see cref="SuggestedPostRefunded"/>: Reason for the refund. Currently, one of <see cref="PostDeleted">PostDeleted</see> if the post was deleted within 24 hours of being posted or removed from scheduled messages without being posted, or <see cref="PaymentRefunded">PaymentRefunded</see> if the payer refunded their payment.</summary>
[JsonConverter(typeof(EnumConverter<SuggestedPostRefundedReason>))]
public enum SuggestedPostRefundedReason
{
    /// <summary>“PostDeleted” reason</summary>
    PostDeleted = 1,
    /// <summary>“PaymentRefunded” reason</summary>
    PaymentRefunded,
}
