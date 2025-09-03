// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes the price of a suggested post.</summary>
public partial class SuggestedPostPrice
{
    /// <summary>Currency in which the post will be paid. Currently, must be one of “XTR” for Telegram Stars or “TON” for toncoins</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Currency { get; set; } = default!;

    /// <summary>The amount of the currency that will be paid for the post in the <em>smallest units</em> of the currency, i.e. Telegram Stars or nanotoncoins. Currently, price in Telegram Stars must be between 5 and 100000, and price in nanotoncoins must be between 10000000 and 10000000000000.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long Amount { get; set; }
}
