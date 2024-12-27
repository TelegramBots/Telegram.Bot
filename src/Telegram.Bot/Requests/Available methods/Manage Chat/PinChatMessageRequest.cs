namespace Telegram.Bot.Requests;

/// <summary>Use this method to add a message to the list of pinned messages in a chat. If the chat is not a private chat, the bot must be an administrator in the chat for this to work and must have the 'CanPinMessages' administrator right in a supergroup or 'CanEditMessages' administrator right in a channel.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class PinChatMessageRequest() : RequestBase<bool>("pinChatMessage"), IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of a message to pin</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>Pass <see langword="true"/> if it is not necessary to send a notification to all chat members about the new pinned message. Notifications are always disabled in channels and private chats.</summary>
    public bool DisableNotification { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message will be pinned</summary>
    public string? BusinessConnectionId { get; set; }
}
