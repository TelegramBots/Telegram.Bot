namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about the completion of a giveaway without public winners.
/// </summary>
public class GiveawayCompleted
{
    /// <summary>
    /// Number of winners in the giveaway
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int WinnerCount { get; set; }

    /// <summary>
    /// Optional. Number of undistributed prizes
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? UnclaimedPrizeCount { get; set; }

    /// <summary>
    /// Optional. Message with the giveaway that was completed, if it wasn't deleted
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Message? GiveawayMessage { get; set; }
}
