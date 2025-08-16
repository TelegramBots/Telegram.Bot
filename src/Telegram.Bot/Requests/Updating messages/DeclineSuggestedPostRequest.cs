// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to decline a suggested post in a direct messages chat. The bot must have the 'CanManageDirectMessages' administrator right in the corresponding channel chat.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class DeclineSuggestedPostRequest() : RequestBase<bool>("declineSuggestedPost"), IChatTargetable
{
    /// <summary>Unique identifier for the target direct messages chat</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long ChatId { get; set; }

    /// <summary>Identifier of a suggested post message to decline</summary>
    [JsonPropertyName("message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>Comment for the creator of the suggested post; 0-128 characters</summary>
    public string? Comment { get; set; }

    /// <inheritdoc/>
    ChatId IChatTargetable.ChatId => ChatId;
}
