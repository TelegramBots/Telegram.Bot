namespace Telegram.Bot.Types;

/// <summary>This object represents a message about the completion of a giveaway with public winners.</summary>
public partial class GiveawayWinners
{
    /// <summary>The chat that created the giveaway</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat Chat { get; set; } = default!;

    /// <summary>Identifier of the message with the giveaway in the chat</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int GiveawayMessageId { get; set; }

    /// <summary>Point in time when winners of the giveaway were selected</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime WinnersSelectionDate { get; set; }

    /// <summary>Total number of winners in the giveaway</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int WinnerCount { get; set; }

    /// <summary>List of up to 100 winners of the giveaway</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User[] Winners { get; set; } = default!;

    /// <summary><em>Optional</em>. The number of other chats the user had to join in order to be eligible for the giveaway</summary>
    public int? AdditionalChatCount { get; set; }

    /// <summary><em>Optional</em>. The number of months the Telegram Premium subscription won from the giveaway will be active for</summary>
    public int? PremiumSubscriptionMonthCount { get; set; }

    /// <summary><em>Optional</em>. Number of undistributed prizes</summary>
    public int? UnclaimedPrizeCount { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if only users who had joined the chats after the giveaway started were eligible to win</summary>
    public bool OnlyNewMembers { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the giveaway was canceled because the payment for it was refunded</summary>
    public bool WasRefunded { get; set; }

    /// <summary><em>Optional</em>. Description of additional giveaway prize</summary>
    public string? PrizeDescription { get; set; }
}
