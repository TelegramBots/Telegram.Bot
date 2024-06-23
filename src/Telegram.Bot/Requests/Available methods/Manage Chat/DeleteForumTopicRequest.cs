namespace Telegram.Bot.Requests;

/// <summary>Use this method to delete a forum topic along with all its messages in a forum supergroup chat. The bot must be an administrator in the chat for this to work and must have the <em>CanDeleteMessages</em> administrator rights.<para>Returns: </para></summary>
public partial class DeleteForumTopicRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier for the target message thread of the forum topic</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageThreadId { get; set; }

    /// <summary>Initializes an instance of <see cref="DeleteForumTopicRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread of the forum topic</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public DeleteForumTopicRequest(ChatId chatId, int messageThreadId) : this()
    {
        ChatId = chatId;
        MessageThreadId = messageThreadId;
    }

    /// <summary>Instantiates a new <see cref="DeleteForumTopicRequest"/></summary>
    public DeleteForumTopicRequest() : base("deleteForumTopic") { }
}
