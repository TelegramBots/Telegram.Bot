namespace Telegram.Bot.Requests;

/// <summary>Use this method to get data for high score tables. Will return the score of the specified user and several of their neighbors in a game.<para>Returns: An Array of <see cref="GameHighScore"/> objects.</para></summary>
/// <remarks>This method will currently return scores for the target user, plus two of their closest neighbors on each side. Will also return the top three users if the user and their neighbors are not among them. Please note that this behavior is subject to change.</remarks>
public partial class GetInlineGameHighScoresRequest : RequestBase<GameHighScore[]>, IUserTargetable
{
    /// <summary>Target user id</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Identifier of the inline message</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string InlineMessageId { get; set; }

    /// <summary>Initializes an instance of <see cref="GetInlineGameHighScoresRequest"/></summary>
    /// <param name="userId">Target user id</param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public GetInlineGameHighScoresRequest(long userId, string inlineMessageId) : this()
    {
        UserId = userId;
        InlineMessageId = inlineMessageId;
    }

    /// <summary>Instantiates a new <see cref="GetInlineGameHighScoresRequest"/></summary>
    public GetInlineGameHighScoresRequest() : base("getGameHighScores") { }
}
