// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to get data for high score tables. Will return the score of the specified user and several of their neighbors in a game.<para>Returns: An Array of <see cref="GameHighScore"/> objects.</para></summary>
/// <remarks>This method will currently return scores for the target user, plus two of their closest neighbors on each side. Will also return the top three users if the user and their neighbors are not among them. Please note that this behavior is subject to change.</remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GetGameHighScoresRequest() : RequestBase<GameHighScore[]>("getGameHighScores"), IChatTargetable, IUserTargetable
{
    /// <summary>Target user id</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Unique identifier for the target chat</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long ChatId { get; set; }

    /// <summary>Identifier of the sent message</summary>
    [JsonPropertyName("message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <inheritdoc/>
    ChatId IChatTargetable.ChatId => ChatId;
}
