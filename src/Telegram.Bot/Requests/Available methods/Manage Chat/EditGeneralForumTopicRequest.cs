namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit the name of the 'General' topic in a forum supergroup chat. The bot must be an administrator in the chat for this to work and must have <em>CanManageTopics</em> administrator rights.<para>Returns: </para></summary>
public partial class EditGeneralForumTopicRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>New topic name, 1-128 characters</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; set; }

    /// <summary>Initializes an instance of <see cref="EditGeneralForumTopicRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="name">New topic name, 1-128 characters</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public EditGeneralForumTopicRequest(ChatId chatId, string name) : this()
    {
        ChatId = chatId;
        Name = name;
    }

    /// <summary>Instantiates a new <see cref="EditGeneralForumTopicRequest"/></summary>
    public EditGeneralForumTopicRequest() : base("editGeneralForumTopic") { }
}
