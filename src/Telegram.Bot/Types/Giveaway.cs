// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a message about a scheduled giveaway.</summary>
public partial class Giveaway
{
    /// <summary>The list of chats which the user must join to participate in the giveaway</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat[] Chats { get; set; } = default!;

    /// <summary>Point in time when winners of the giveaway will be selected</summary>
    [JsonPropertyName("winners_selection_date")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime WinnersSelectionDate { get; set; }

    /// <summary>The number of users which are supposed to be selected as winners of the giveaway</summary>
    [JsonPropertyName("winner_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int WinnerCount { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if only users who join the chats after the giveaway started should be eligible to win</summary>
    [JsonPropertyName("only_new_members")]
    public bool OnlyNewMembers { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the list of giveaway winners will be visible to everyone</summary>
    [JsonPropertyName("has_public_winners")]
    public bool HasPublicWinners { get; set; }

    /// <summary><em>Optional</em>. Description of additional giveaway prize</summary>
    [JsonPropertyName("prize_description")]
    public string? PrizeDescription { get; set; }

    /// <summary><em>Optional</em>. A list of two-letter <a href="https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">ISO 3166-1 alpha-2</a> country codes indicating the countries from which eligible users for the giveaway must come. If empty, then all users can participate in the giveaway. Users with a phone number that was bought on Fragment can always participate in giveaways.</summary>
    [JsonPropertyName("country_codes")]
    public string[]? CountryCodes { get; set; }

    /// <summary><em>Optional</em>. The number of Telegram Stars to be split between giveaway winners; for Telegram Star giveaways only</summary>
    [JsonPropertyName("prize_star_count")]
    public long? PrizeStarCount { get; set; }

    /// <summary><em>Optional</em>. The number of months the Telegram Premium subscription won from the giveaway will be active for; for Telegram Premium giveaways only</summary>
    [JsonPropertyName("premium_subscription_month_count")]
    public int? PremiumSubscriptionMonthCount { get; set; }
}
