// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Payments;

/// <summary>Contains a list of Telegram Star transactions.</summary>
public partial class StarTransactions
{
    /// <summary>The list of transactions</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public StarTransaction[] Transactions { get; set; } = default!;

    /// <summary>Implicit conversion to StarTransaction[] (Transactions)</summary>
    public static implicit operator StarTransaction[](StarTransactions self) => self.Transactions;
    /// <summary>Implicit conversion from StarTransaction[] (Transactions)</summary>
    public static implicit operator StarTransactions(StarTransaction[] transactions) => new() { Transactions = transactions };
}
