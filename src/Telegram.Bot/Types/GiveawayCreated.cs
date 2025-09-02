// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a service message about the creation of a scheduled giveaway.</summary>
public partial class GiveawayCreated
{
    /// <summary><em>Optional</em>. The number of Telegram Stars to be split between giveaway winners; for Telegram Star giveaways only</summary>
    [JsonPropertyName("prize_star_count")]
    public long? PrizeStarCount { get; set; }

    /// <summary>Implicit conversion to long (PrizeStarCount)</summary>
    public static implicit operator long?(GiveawayCreated self) => self.PrizeStarCount;
    /// <summary>Implicit conversion from long (PrizeStarCount)</summary>
    public static implicit operator GiveawayCreated(long? prizeStarCount) => new() { PrizeStarCount = prizeStarCount };
}
