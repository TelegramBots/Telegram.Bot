namespace Telegram.Bot.Types;

/// <summary>
/// This object represents an answer of a user in a non-anonymous poll.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class PollAnswer
{
    /// <summary>
    /// Unique poll identifier
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string PollId { get; set; } = default!;

    /// <summary>
    /// Optional. The chat that changed the answer to the poll, if the voter is anonymous
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Chat? VoterChat { get; set; }

    /// <summary>
    /// Optional. The user that changed the answer to the poll, if the voter isn't anonymous
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public User? User { get; set; }

    /// <summary>
    /// 0-based identifiers of answer options, chosen by the user. May be empty if the user retracted their vote.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int[] OptionIds { get; set; } = default!;
}
