namespace Telegram.Bot.Requests;

/// <summary>Use this method to clear the list of pinned messages in a forum topic. The bot must be an administrator in the chat for this to work and must have the <em>CanPinMessages</em> administrator right in the supergroup.<para>Returns: </para></summary>
public partial class UnpinAllForumTopicMessagesRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier for the target message thread of the forum topic</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageThreadId { get; set; }

    /// <summary>Initializes an instance of <see cref="UnpinAllForumTopicMessagesRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread of the forum topic</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public UnpinAllForumTopicMessagesRequest(ChatId chatId, int messageThreadId) : this()
    {
        ChatId = chatId;
        MessageThreadId = messageThreadId;
    }

    /// <summary>Instantiates a new <see cref="UnpinAllForumTopicMessagesRequest"/></summary>
    public UnpinAllForumTopicMessagesRequest() : base("unpinAllForumTopicMessages") { }
}
