// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to set the score of the specified user in a game message.<para>Returns: The <see cref="Message"/> is returned</para></summary>
/// <remarks>Returns an error, if the new score is not greater than the user's current score in the chat and <see cref="Force">Force</see> is <em>False</em>.</remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetGameScoreRequest() : RequestBase<Message>("setGameScore"), IChatTargetable, IUserTargetable
{
    /// <summary>User identifier</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>New score, must be non-negative</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int Score { get; set; }

    /// <summary>Unique identifier for the target chat</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long ChatId { get; set; }

    /// <summary>Identifier of the sent message</summary>
    [JsonPropertyName("message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>Pass <see langword="true"/> if the high score is allowed to decrease. This can be useful when fixing mistakes or banning cheaters</summary>
    public bool Force { get; set; }

    /// <summary>Pass <see langword="true"/> if the game message should not be automatically edited to include the current scoreboard</summary>
    [JsonPropertyName("disable_edit_message")]
    public bool DisableEditMessage { get; set; }

    /// <inheritdoc/>
    ChatId IChatTargetable.ChatId => ChatId;
}
