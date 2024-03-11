using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a message about the completion of a giveaway with public winners.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class GiveawayWinners
{
    /// <summary>
    /// The chat that created the giveaway
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public Chat Chat { get; set; } = default!;

    /// <summary>
    /// Identifier of the message with the giveaway in the chat
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int GiveawayMessageId { get; set; }

    /// <summary>
    /// Point in time when winners of the giveaway were selected
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime WinnersSelectionDate { get; set; }

    /// <summary>
    /// Total number of winners in the giveaway
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int WinnerCount { get; set; }

    /// <summary>
    /// List of up to 100 winners of the giveaway
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public User[] Winners { get; set; } = default!;

    /// <summary>
    /// Optional. The number of other chats the user had to join in order to be eligible for the giveaway
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? AdditionalChatCount { get; set; }

    /// <summary>
    /// Optional. The number of months the Telegram Premium subscription won from the giveaway will be active for
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? PremiumSubscriptionMonthCount { get; set; }

    /// <summary>
    /// Optional. Number of undistributed prizes
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? UnclaimedPrizeCount { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if only users who had joined the chats after the giveaway started were eligible to win
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? OnlyNewMembers { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the giveaway was canceled because the payment for it was refunded
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? WasRefunded { get; set; }

    /// <summary>
    /// Optional. Description of additional giveaway prize
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? PrizeDescription { get; set; }
}
