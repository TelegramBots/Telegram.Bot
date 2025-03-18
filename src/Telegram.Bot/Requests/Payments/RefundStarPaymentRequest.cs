// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Refunds a successful payment in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class RefundStarPaymentRequest() : RequestBase<bool>("refundStarPayment"), IUserTargetable
{
    /// <summary>Identifier of the user whose payment will be refunded</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Telegram payment identifier</summary>
    [JsonPropertyName("telegram_payment_charge_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string TelegramPaymentChargeId { get; set; }
}
