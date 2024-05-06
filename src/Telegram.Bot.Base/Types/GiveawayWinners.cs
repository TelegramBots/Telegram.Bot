using Telegram.Bot.Serialization;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a message about the completion of a giveaway with public winners.
/// </summary>
public class GiveawayWinners
{
    /// <summary>
    /// The chat that created the giveaway
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat Chat { get; set; } = default!;

    /// <summary>
    /// Identifier of the message with the giveaway in the chat
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int GiveawayMessageId { get; set; }

    /// <summary>
    /// Point in time when winners of the giveaway were selected
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime WinnersSelectionDate { get; set; }

    /// <summary>
    /// Total number of winners in the giveaway
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int WinnerCount { get; set; }

    /// <summary>
    /// List of up to 100 winners of the giveaway
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User[] Winners { get; set; } = default!;

    /// <summary>
    /// Optional. The number of other chats the user had to join in order to be eligible for the giveaway
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? AdditionalChatCount { get; set; }

    /// <summary>
    /// Optional. The number of months the Telegram Premium subscription won from the giveaway will be active for
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PremiumSubscriptionMonthCount { get; set; }

    /// <summary>
    /// Optional. Number of undistributed prizes
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? UnclaimedPrizeCount { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if only users who had joined the chats after the giveaway started were eligible to win
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? OnlyNewMembers { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the giveaway was canceled because the payment for it was refunded
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? WasRefunded { get; set; }

    /// <summary>
    /// Optional. Description of additional giveaway prize
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PrizeDescription { get; set; }
}
