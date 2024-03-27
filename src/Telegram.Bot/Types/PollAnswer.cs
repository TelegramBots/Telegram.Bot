namespace Telegram.Bot.Types;

/// <summary>
/// This object represents an answer of a user in a non-anonymous poll.
/// </summary>
public class PollAnswer
{
    /// <summary>
    /// Unique poll identifier
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string PollId { get; set; } = default!;

    /// <summary>
    /// Optional. The chat that changed the answer to the poll, if the voter is anonymous
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Chat? VoterChat { get; set; }

    /// <summary>
    /// Optional. The user that changed the answer to the poll, if the voter isn't anonymous
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public User? User { get; set; }

    /// <summary>
    /// 0-based identifiers of answer options, chosen by the user. May be empty if the user retracted their vote.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int[] OptionIds { get; set; } = default!;
}
