namespace Telegram.Bot.Requests;

/// <summary>Use this method to approve a chat join request. The bot must be an administrator in the chat for this to work and must have the <em>CanInviteUsers</em> administrator right.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class ApproveChatJoinRequest() : RequestBase<bool>("approveChatJoinRequest"), IChatTargetable, IUserTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the target user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }
}
