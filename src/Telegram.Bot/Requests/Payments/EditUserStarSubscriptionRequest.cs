// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Allows the bot to cancel or re-enable extension of a subscription paid in Telegram Stars.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class EditUserStarSubscriptionRequest() : RequestBase<bool>("editUserStarSubscription"), IUserTargetable
{
    /// <summary>Identifier of the user whose subscription will be edited</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Telegram payment identifier for the subscription</summary>
    [JsonPropertyName("telegram_payment_charge_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string TelegramPaymentChargeId { get; set; }

    /// <summary>Pass <see langword="true"/> to cancel extension of the user subscription; the subscription must be active up to the end of the current subscription period. Pass <see langword="false"/> to allow the user to re-enable a subscription that was previously canceled by the bot.</summary>
    [JsonPropertyName("is_canceled")]
    public required bool IsCanceled { get; set; }
}
