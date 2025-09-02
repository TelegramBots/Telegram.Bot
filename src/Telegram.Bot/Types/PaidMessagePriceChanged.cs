// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about a change in the price of paid messages within a chat.</summary>
public partial class PaidMessagePriceChanged
{
    /// <summary>The new number of Telegram Stars that must be paid by non-administrator users of the supergroup chat for each sent message</summary>
    [JsonPropertyName("paid_message_star_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long PaidMessageStarCount { get; set; }

    /// <summary>Implicit conversion to long (PaidMessageStarCount)</summary>
    public static implicit operator long(PaidMessagePriceChanged self) => self.PaidMessageStarCount;
    /// <summary>Implicit conversion from long (PaidMessageStarCount)</summary>
    public static implicit operator PaidMessagePriceChanged(long paidMessageStarCount) => new() { PaidMessageStarCount = paidMessageStarCount };
}
