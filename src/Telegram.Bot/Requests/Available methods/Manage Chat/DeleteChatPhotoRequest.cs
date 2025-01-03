namespace Telegram.Bot.Requests;

/// <summary>Use this method to delete a chat photo. Photos can't be changed for private chats. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class DeleteChatPhotoRequest() : RequestBase<bool>("deleteChatPhoto"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }
}
