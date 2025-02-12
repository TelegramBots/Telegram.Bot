// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Payments;

/// <summary>Type of the state, always <see cref="RevenueWithdrawalState"/></summary>
[JsonConverter(typeof(EnumConverter<RevenueWithdrawalStateType>))]
public enum RevenueWithdrawalStateType
{
    /// <summary>The withdrawal is in progress.<br/><br/><i>(<see cref="RevenueWithdrawalState"/> can be cast into <see cref="RevenueWithdrawalStatePending"/>)</i></summary>
    Pending = 1,
    /// <summary>The withdrawal succeeded.<br/><br/><i>(<see cref="RevenueWithdrawalState"/> can be cast into <see cref="RevenueWithdrawalStateSucceeded"/>)</i></summary>
    Succeeded,
    /// <summary>The withdrawal failed and the transaction was refunded.<br/><br/><i>(<see cref="RevenueWithdrawalState"/> can be cast into <see cref="RevenueWithdrawalStateFailed"/>)</i></summary>
    Failed,
}
