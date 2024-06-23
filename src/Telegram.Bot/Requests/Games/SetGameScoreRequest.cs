namespace Telegram.Bot.Requests;

/// <summary>Use this method to set the score of the specified user in a game message.<para>Returns: The <see cref="Message"/> is returned</para></summary>
/// <remarks>Returns an error, if the new score is not greater than the user's current score in the chat and <see cref="Force">Force</see> is <em>False</em>.</remarks>
public partial class SetGameScoreRequest : RequestBase<Message>, IChatTargetable, IUserTargetable
{
    /// <summary>User identifier</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>New score, must be non-negative</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int Score { get; set; }

    /// <summary>Unique identifier for the target chat</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long ChatId { get; set; }

    /// <summary>Identifier of the sent message</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>Pass <see langword="true"/> if the high score is allowed to decrease. This can be useful when fixing mistakes or banning cheaters</summary>
    public bool Force { get; set; }

    /// <summary>Pass <see langword="true"/> if the game message should not be automatically edited to include the current scoreboard</summary>
    public bool DisableEditMessage { get; set; }

    /// <summary>Initializes an instance of <see cref="SetGameScoreRequest"/></summary>
    /// <param name="userId">User identifier</param>
    /// <param name="score">New score, must be non-negative</param>
    /// <param name="chatId">Unique identifier for the target chat</param>
    /// <param name="messageId">Identifier of the sent message</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SetGameScoreRequest(long userId, int score, long chatId, int messageId) : this()
    {
        UserId = userId;
        Score = score;
        ChatId = chatId;
        MessageId = messageId;
    }

    /// <summary>Instantiates a new <see cref="SetGameScoreRequest"/></summary>
    public SetGameScoreRequest() : base("setGameScore") { }

    /// <inheritdoc />
    ChatId IChatTargetable.ChatId => ChatId;
}
