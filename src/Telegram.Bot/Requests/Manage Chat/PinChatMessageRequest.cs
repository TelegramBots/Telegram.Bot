// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to add a message to the list of pinned messages in a chat. In private chats and channel direct messages chats, all non-service messages can be pinned. Conversely, the bot must be an administrator with the 'CanPinMessages' right or the 'CanEditMessages' right to pin messages in groups and channels respectively.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class PinChatMessageRequest() : RequestBase<bool>("pinChatMessage"), IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of a message to pin</summary>
    [JsonPropertyName("message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message will be pinned</summary>
    [JsonPropertyName("business_connection_id")]
    public string? BusinessConnectionId { get; set; }

    /// <summary>Pass <see langword="true"/> if it is not necessary to send a notification to all chat members about the new pinned message. Notifications are always disabled in channels and private chats.</summary>
    [JsonPropertyName("disable_notification")]
    public bool DisableNotification { get; set; }
}
