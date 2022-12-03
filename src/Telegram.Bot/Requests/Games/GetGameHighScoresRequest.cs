using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to get data for high score tables. Will return the score of the specified user
/// and several of their neighbors in a game. On success, returns an Array of
/// <see cref="GameHighScore"/> objects.
/// </summary>
/// <remarks>
/// This method will currently return scores for the target user, plus two of their closest neighbors
/// on each side. Will also return the top three users if the user and his neighbors are not among
/// them. Please note that this behavior is subject to change.
/// </remarks>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class GetGameHighScoresRequest : RequestBase<GameHighScore[]>, IUserTargetable, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public long UserId { get; }

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
    /// Initializes a new request with userId, chatId and messageId
    /// </summary>
    /// <param name="userId">Target user id</param>
    /// <param name="chatId">Unique identifier for the target chat</param>
    /// <param name="messageId">Identifier of the sent message</param>
    public GetGameHighScoresRequest(long userId, long chatId, int messageId)
        : base("getGameHighScores")
    {
        UserId = userId;
        ChatId = chatId;
        MessageId = messageId;
    }
}
