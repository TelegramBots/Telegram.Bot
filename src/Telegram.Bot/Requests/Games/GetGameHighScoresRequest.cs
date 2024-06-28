namespace Telegram.Bot.Requests;

/// <summary>Use this method to get data for high score tables. Will return the score of the specified user and several of their neighbors in a game.<para>Returns: An Array of <see cref="GameHighScore"/> objects.</para></summary>
/// <remarks>This method will currently return scores for the target user, plus two of their closest neighbors on each side. Will also return the top three users if the user and their neighbors are not among them. Please note that this behavior is subject to change.</remarks>
public partial class GetGameHighScoresRequest : RequestBase<GameHighScore[]>, IChatTargetable, IUserTargetable
{
    /// <summary>Target user id</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Unique identifier for the target chat</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long ChatId { get; set; }

    /// <summary>Identifier of the sent message</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>Initializes an instance of <see cref="GetGameHighScoresRequest"/></summary>
    /// <param name="userId">Target user id</param>
    /// <param name="chatId">Unique identifier for the target chat</param>
    /// <param name="messageId">Identifier of the sent message</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public GetGameHighScoresRequest(long userId, long chatId, int messageId) : this()
    {
        UserId = userId;
        ChatId = chatId;
        MessageId = messageId;
    }

    /// <summary>Instantiates a new <see cref="GetGameHighScoresRequest"/></summary>
    public GetGameHighScoresRequest() : base("getGameHighScores") { }

    /// <inheritdoc />
    ChatId IChatTargetable.ChatId => ChatId;
}
