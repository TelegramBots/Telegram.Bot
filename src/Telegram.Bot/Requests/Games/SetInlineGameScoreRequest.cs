using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to set the score of the specified user in a game. On success returns <see langword="true"/>.
/// Returns an error, if the new score is not greater than the user's current score in the chat and
/// <see cref="Force"/> is <see langword="false"/>.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SetInlineGameScoreRequest : RequestBase<bool>, IUserTargetable
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
    /// Pass <see langword="true"/>, if the game message should not be automatically edited to include the current
    /// scoreboard
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? DisableEditMessage { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.InlineMessageId"/>
    [JsonProperty(Required = Required.Always)]
    public string InlineMessageId { get; }

    /// <summary>
    /// Initializes a new request with userId, inlineMessageId and new score
    /// </summary>
    /// <param name="userId">User identifier</param>
    /// <param name="score">New score, must be non-negative</param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    public SetInlineGameScoreRequest(long userId, int score, string inlineMessageId)
        : base("setGameScore")
    {
        UserId = userId;
        Score = score;
        InlineMessageId = inlineMessageId;
    }
}
