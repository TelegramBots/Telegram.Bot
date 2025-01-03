namespace Telegram.Bot.Requests;

/// <summary>Use this method for your bot to leave a group, supergroup or channel.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class LeaveChatRequest() : RequestBase<bool>("leaveChat"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }
}
