namespace Telegram.Bot.Requests;

/// <summary>Use this method to change the description of a group, a supergroup or a channel. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetChatDescriptionRequest() : RequestBase<bool>("setChatDescription"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>New chat description, 0-255 characters</summary>
    public string? Description { get; set; }
}
