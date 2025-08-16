// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to clear the list of pinned messages in a chat. In private chats and channel direct messages chats, no additional rights are required to unpin all pinned messages. Conversely, the bot must be an administrator with the 'CanPinMessages' right or the 'CanEditMessages' right to unpin all pinned messages in groups and channels respectively.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class UnpinAllChatMessagesRequest() : RequestBase<bool>("unpinAllChatMessages"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }
}
