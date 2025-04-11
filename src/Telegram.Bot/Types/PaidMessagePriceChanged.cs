// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about a change in the price of paid messages within a chat.</summary>
public partial class PaidMessagePriceChanged
{
    /// <summary>The new number of Telegram Stars that must be paid by non-administrator users of the supergroup chat for each sent message</summary>
    [JsonPropertyName("paid_message_star_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int PaidMessageStarCount { get; set; }

    /// <summary>Implicit conversion to int (PaidMessageStarCount)</summary>
    public static implicit operator int(PaidMessagePriceChanged self) => self.PaidMessageStarCount;
    /// <summary>Implicit conversion from int (PaidMessageStarCount)</summary>
    public static implicit operator PaidMessagePriceChanged(int paidMessageStarCount) => new() { PaidMessageStarCount = paidMessageStarCount };
}
