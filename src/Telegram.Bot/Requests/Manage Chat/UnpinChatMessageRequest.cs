// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to remove a message from the list of pinned messages in a chat. In private chats and channel direct messages chats, all messages can be unpinned. Conversely, the bot must be an administrator with the 'CanPinMessages' right or the 'CanEditMessages' right to unpin messages in groups and channels respectively.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class UnpinChatMessageRequest() : RequestBase<bool>("unpinChatMessage"), IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message will be unpinned</summary>
    [JsonPropertyName("business_connection_id")]
    public string? BusinessConnectionId { get; set; }

    /// <summary>Identifier of the message to unpin. Required if <see cref="BusinessConnectionId">BusinessConnectionId</see> is specified. If not specified, the most recent pinned message (by sending date) will be unpinned.</summary>
    [JsonPropertyName("message_id")]
    public int? MessageId { get; set; }
}
