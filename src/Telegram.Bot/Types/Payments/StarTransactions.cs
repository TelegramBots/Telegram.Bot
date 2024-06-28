namespace Telegram.Bot.Types.Payments;

/// <summary>Contains a list of Telegram Star transactions.</summary>
public partial class StarTransactions
{
    /// <summary>The list of transactions</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public StarTransaction[] Transactions { get; set; } = default!;
}
