namespace Telegram.Bot.Requests;

/// <summary>Use this method to remove a message from the list of pinned messages in a chat. If the chat is not a private chat, the bot must be an administrator in the chat for this to work and must have the 'CanPinMessages' administrator right in a supergroup or 'CanEditMessages' administrator right in a channel.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class UnpinChatMessageRequest() : RequestBase<bool>("unpinChatMessage"), IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of the message to unpin. Required if <see cref="BusinessConnectionId">BusinessConnectionId</see> is specified. If not specified, the most recent pinned message (by sending date) will be unpinned.</summary>
    public int? MessageId { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message will be unpinned</summary>
    public string? BusinessConnectionId { get; set; }
}
