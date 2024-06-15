using Telegram.Bot.Serialization;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a message about a scheduled giveaway.
/// </summary>
public class Giveaway
{
    /// <summary>
    /// The list of chats which the user must join to participate in the giveaway
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat[] Chats { get; set; } = default!;

    /// <summary>
    /// Point in time when winners of the giveaway will be selected
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime WinnersSelectionDate { get; set; }

    /// <summary>
    /// The number of users which are supposed to be selected as winners of the giveaway
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int WinnerCount { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if only users who join the chats after the giveaway started should be eligible to win
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? OnlyNewMembers { get; set; }

    /// <summary>
    /// Optional.<see langword="true"/>, if the list of giveaway winners will be visible to everyone
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? HasPublicWinners { get; set; }

    /// <summary>
    /// Optional. Description of additional giveaway prize
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PrizeDescription { get; set; }

    /// <summary>
    /// Optional.A list of two-letter <see href="https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">ISO 3166-1 alpha-2</see>
    /// country codes indicating the countries from which eligible users for the giveaway must come.
    /// If empty, then all users can participate in the giveaway.
    /// Users with a phone number that was bought on Fragment can always participate in giveaways.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? CountryCodes { get; set; }

    /// <summary>
    /// Optional. The number of months the Telegram Premium subscription won from the giveaway will be active for
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PremiumSubscriptionMonthCount { get; set; }
}
