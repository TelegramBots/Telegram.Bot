namespace Telegram.Bot.Requests;

/// <summary>Use this method to unhide the 'General' topic in a forum supergroup chat. The bot must be an administrator in the chat for this to work and must have the <em>CanManageTopics</em> administrator rights.<para>Returns: </para></summary>
public partial class UnhideGeneralForumTopicRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Instantiates a new <see cref="UnhideGeneralForumTopicRequest"/></summary>
    public UnhideGeneralForumTopicRequest() : base("unhideGeneralForumTopic") { }
}
