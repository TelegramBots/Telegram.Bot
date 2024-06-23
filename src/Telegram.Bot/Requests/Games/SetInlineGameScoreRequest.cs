namespace Telegram.Bot.Requests;

/// <summary>Use this method to set the score of the specified user in a game message.<para>Returns: </para></summary>
/// <remarks>Returns an error, if the new score is not greater than the user's current score in the chat and <see cref="Force">Force</see> is <em>False</em>.</remarks>
public partial class SetInlineGameScoreRequest : RequestBase<bool>, IUserTargetable
{
    /// <summary>User identifier</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>New score, must be non-negative</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int Score { get; set; }

    /// <summary>Identifier of the inline message</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string InlineMessageId { get; set; }

    /// <summary>Pass <see langword="true"/> if the high score is allowed to decrease. This can be useful when fixing mistakes or banning cheaters</summary>
    public bool Force { get; set; }

    /// <summary>Pass <see langword="true"/> if the game message should not be automatically edited to include the current scoreboard</summary>
    public bool DisableEditMessage { get; set; }

    /// <summary>Initializes an instance of <see cref="SetInlineGameScoreRequest"/></summary>
    /// <param name="userId">User identifier</param>
    /// <param name="score">New score, must be non-negative</param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SetInlineGameScoreRequest(long userId, int score, string inlineMessageId) : this()
    {
        UserId = userId;
        Score = score;
        InlineMessageId = inlineMessageId;
    }

    /// <summary>Instantiates a new <see cref="SetInlineGameScoreRequest"/></summary>
    public SetInlineGameScoreRequest() : base("setGameScore") { }
}
