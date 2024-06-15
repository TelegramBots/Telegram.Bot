namespace Telegram.Bot.Types;

/// <summary>
/// This object contains information about one answer option in a poll.
/// </summary>
public class PollOption
{
    /// <summary>
    /// Option text, 1-100 characters
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Text { get; set; } = default!;

    /// <summary>
    /// Optional. Special entities that appear in the option text.
    /// Currently, only custom emoji entities are allowed in poll option texts
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MessageEntity[]? TextEntities { get; set; }

    /// <summary>
    /// Number of users that voted for this option
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int VoterCount { get; set; }
}
