using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to set the score of the specified user in a game. On success returns the edited
/// <see cref="Message"/>. Returns an error, if the new score is not greater than the user's current
/// score in the chat and <see cref="Force"/> is <see langword="false"/>.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SetGameScoreRequest : RequestBase<Message>, IUserTargetable, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public long UserId { get; }

    /// <summary>
    /// New score, must be non-negative
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Score { get; }

    /// <summary>
    /// Pass <see langword="true"/>, if the high score is allowed to decrease. This can be useful when fixing mistakes
    /// or banning cheaters.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? Force { get; set; }

    /// <summary>
    /// Pass <see langword="true"/>, if the game message should not be automatically edited to include
    /// the current scoreboard
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? DisableEditMessage { get; set; }

    /// <summary>
    /// Unique identifier for the target chat
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public long ChatId { get; }

    /// <inheritdoc />
    ChatId IChatTargetable.ChatId => ChatId;

    /// <summary>
    /// Identifier of the sent message
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int MessageId { get; }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    /// <param name="userId">User identifier</param>
    /// <param name="score">New score, must be non-negative</param>
    /// <param name="chatId">Unique identifier for the target chat</param>
    /// <param name="messageId">Identifier of the sent message</param>
    public SetGameScoreRequest(long userId, int score, long chatId, int messageId)
        : base("setGameScore")
    {
        UserId = userId;
        Score = score;
        ChatId = chatId;
        MessageId = messageId;
    }
}
