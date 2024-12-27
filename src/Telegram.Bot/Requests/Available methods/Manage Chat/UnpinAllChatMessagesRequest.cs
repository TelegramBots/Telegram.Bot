namespace Telegram.Bot.Requests;

/// <summary>Use this method to clear the list of pinned messages in a chat. If the chat is not a private chat, the bot must be an administrator in the chat for this to work and must have the 'CanPinMessages' administrator right in a supergroup or 'CanEditMessages' administrator right in a channel.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class UnpinAllChatMessagesRequest() : RequestBase<bool>("unpinAllChatMessages"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }
}
