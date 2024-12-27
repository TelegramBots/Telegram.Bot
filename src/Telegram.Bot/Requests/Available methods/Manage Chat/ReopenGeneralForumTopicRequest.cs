namespace Telegram.Bot.Requests;

/// <summary>Use this method to reopen a closed 'General' topic in a forum supergroup chat. The bot must be an administrator in the chat for this to work and must have the <em>CanManageTopics</em> administrator rights. The topic will be automatically unhidden if it was hidden.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class ReopenGeneralForumTopicRequest() : RequestBase<bool>("reopenGeneralForumTopic"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }
}
