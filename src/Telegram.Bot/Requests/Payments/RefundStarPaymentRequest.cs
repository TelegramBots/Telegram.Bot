using JetBrains.Annotations;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Refunds a successful payment in Telegram Stars. Returns <see langword="true"/> on success.
/// </summary>
[PublicAPI]
public class RefundStarPaymentRequest : RequestBase<bool>, IUserTargetable
{
    /// <summary>
    /// Identifier of the user whose payment will be refunded
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; init; }

    /// <summary>
    /// Telegram payment identifier
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string TelegramPaymentChargeId { get; init; }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public RefundStarPaymentRequest()
        : base("refundStarPayment")
    { }
}
