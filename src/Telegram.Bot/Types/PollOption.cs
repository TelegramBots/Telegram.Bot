namespace Telegram.Bot.Types;

/// <summary>This object contains information about one answer option in a poll.</summary>
public partial class PollOption
{
    /// <summary>Option text, 1-100 characters</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Text { get; set; } = default!;

    /// <summary><em>Optional</em>. Special entities that appear in the option <see cref="Text">Text</see>. Currently, only custom emoji entities are allowed in poll option texts</summary>
    public MessageEntity[]? TextEntities { get; set; }

    /// <summary>Number of users that voted for this option</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int VoterCount { get; set; }
}
