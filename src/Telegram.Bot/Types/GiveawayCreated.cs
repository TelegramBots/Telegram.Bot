namespace Telegram.Bot.Types;

/// <summary>This object represents a service message about the creation of a scheduled giveaway.</summary>
public partial class GiveawayCreated
{
    /// <summary><em>Optional</em>. The number of Telegram Stars to be split between giveaway winners; for Telegram Star giveaways only</summary>
    public int? PrizeStarCount { get; set; }
}
