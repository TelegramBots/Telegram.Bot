namespace Telegram.Bot.Types;

/// <summary>This object represents an answer of a user in a non-anonymous poll.</summary>
public partial class PollAnswer
{
    /// <summary>Unique poll identifier</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string PollId { get; set; } = default!;

    /// <summary><em>Optional</em>. The chat that changed the answer to the poll, if the voter is anonymous</summary>
    public Chat? VoterChat { get; set; }

    /// <summary><em>Optional</em>. The user that changed the answer to the poll, if the voter isn't anonymous</summary>
    public User? User { get; set; }

    /// <summary>0-based identifiers of chosen answer options. May be empty if the vote was retracted.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int[] OptionIds { get; set; } = default!;
}
