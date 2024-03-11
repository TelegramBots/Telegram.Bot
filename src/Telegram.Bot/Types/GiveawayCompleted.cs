namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about the completion of a giveaway without public winners.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class GiveawayCompleted
{
    /// <summary>
    /// Number of winners in the giveaway
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int WinnerCount { get; set; }

    /// <summary>
    /// Optional. Number of undistributed prizes
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? UnclaimedPrizeCount { get; set; }

    /// <summary>
    /// Optional. Message with the giveaway that was completed, if it wasn't deleted
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Message? GiveawayMessage { get; set; }
}
