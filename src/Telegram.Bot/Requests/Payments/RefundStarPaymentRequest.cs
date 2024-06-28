namespace Telegram.Bot.Requests;

/// <summary>Refunds a successful payment in <a href="https://t.me/BotNews/90">Telegram Stars</a>.<para>Returns: </para></summary>
public partial class RefundStarPaymentRequest : RequestBase<bool>, IUserTargetable
{
    /// <summary>Identifier of the user whose payment will be refunded</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Telegram payment identifier</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string TelegramPaymentChargeId { get; set; }

    /// <summary>Initializes an instance of <see cref="RefundStarPaymentRequest"/></summary>
    /// <param name="userId">Identifier of the user whose payment will be refunded</param>
    /// <param name="telegramPaymentChargeId">Telegram payment identifier</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public RefundStarPaymentRequest(long userId, string telegramPaymentChargeId) : this()
    {
        UserId = userId;
        TelegramPaymentChargeId = telegramPaymentChargeId;
    }

    /// <summary>Instantiates a new <see cref="RefundStarPaymentRequest"/></summary>
    public RefundStarPaymentRequest() : base("refundStarPayment") { }
}
