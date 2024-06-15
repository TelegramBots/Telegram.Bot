using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to set the score of the specified user in a game. On success returns the edited
/// <see cref="Message"/>. Returns an error, if the new score is not greater than the user's current
/// score in the chat and <see cref="Force"/> is <see langword="false"/>.
/// </summary>
public class SetGameScoreRequest : RequestBase<Message>, IUserTargetable, IChatTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; init; }

    /// <summary>
    /// New score, must be non-negative
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int Score { get; init; }

    /// <summary>
    /// Pass <see langword="true"/>, if the high score is allowed to decrease. This can be useful when fixing mistakes
    /// or banning cheaters.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Force { get; set; }

    /// <summary>
    /// Pass <see langword="true"/>, if the game message should not be automatically edited to include
    /// the current scoreboard
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? DisableEditMessage { get; set; }

    /// <summary>
    /// Unique identifier for the target chat
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long ChatId { get; init; }

    /// <inheritdoc />
    ChatId IChatTargetable.ChatId => ChatId;

    /// <summary>
    /// Identifier of the sent message
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; init; }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    /// <param name="userId">User identifier</param>
    /// <param name="score">New score, must be non-negative</param>
    /// <param name="chatId">Unique identifier for the target chat</param>
    /// <param name="messageId">Identifier of the sent message</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public SetGameScoreRequest(long userId, int score, long chatId, int messageId)
        : this()
    {
        UserId = userId;
        Score = score;
        ChatId = chatId;
        MessageId = messageId;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public SetGameScoreRequest()
        : base("setGameScore")
    { }
}
