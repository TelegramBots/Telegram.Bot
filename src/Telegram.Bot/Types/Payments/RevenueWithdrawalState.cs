// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Payments;

/// <summary>This object describes the state of a revenue withdrawal operation. Currently, it can be one of<br/><see cref="RevenueWithdrawalStatePending"/>, <see cref="RevenueWithdrawalStateSucceeded"/>, <see cref="RevenueWithdrawalStateFailed"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<RevenueWithdrawalState>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(RevenueWithdrawalStatePending), "pending")]
[CustomJsonDerivedType(typeof(RevenueWithdrawalStateSucceeded), "succeeded")]
[CustomJsonDerivedType(typeof(RevenueWithdrawalStateFailed), "failed")]
public abstract partial class RevenueWithdrawalState
{
    /// <summary>Type of the state, always <see cref="RevenueWithdrawalState"/></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract RevenueWithdrawalStateType Type { get; }
}

/// <summary>The withdrawal is in progress.</summary>
public partial class RevenueWithdrawalStatePending : RevenueWithdrawalState
{
    /// <summary>Type of the state, always <see cref="RevenueWithdrawalStateType.Pending"/></summary>
    public override RevenueWithdrawalStateType Type => RevenueWithdrawalStateType.Pending;
}

/// <summary>The withdrawal succeeded.</summary>
public partial class RevenueWithdrawalStateSucceeded : RevenueWithdrawalState
{
    /// <summary>Type of the state, always <see cref="RevenueWithdrawalStateType.Succeeded"/></summary>
    public override RevenueWithdrawalStateType Type => RevenueWithdrawalStateType.Succeeded;

    /// <summary>Date the withdrawal was completed</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Date { get; set; }

    /// <summary>An HTTPS URL that can be used to see transaction details</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Url { get; set; } = default!;
}

/// <summary>The withdrawal failed and the transaction was refunded.</summary>
public partial class RevenueWithdrawalStateFailed : RevenueWithdrawalState
{
    /// <summary>Type of the state, always <see cref="RevenueWithdrawalStateType.Failed"/></summary>
    public override RevenueWithdrawalStateType Type => RevenueWithdrawalStateType.Failed;
}
