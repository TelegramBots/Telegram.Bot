// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes an amount of Telegram Stars.</summary>
public partial class StarAmount
{
    /// <summary>Integer amount of Telegram Stars, rounded to 0; can be negative</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long Amount { get; set; }

    /// <summary><em>Optional</em>. The number of 1/1000000000 shares of Telegram Stars; from -999999999 to 999999999; can be negative if and only if <see cref="Amount">Amount</see> is non-positive</summary>
    [JsonPropertyName("nanostar_amount")]
    public long? NanostarAmount { get; set; }
}
