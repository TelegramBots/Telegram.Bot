namespace Telegram.Bot.Requests;

/// <summary>Refunds a successful payment in <a href="https://t.me/BotNews/90">Telegram Stars</a>.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class RefundStarPaymentRequest() : RequestBase<bool>("refundStarPayment"), IUserTargetable
{
    /// <summary>Identifier of the user whose payment will be refunded</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Telegram payment identifier</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string TelegramPaymentChargeId { get; set; }
}
