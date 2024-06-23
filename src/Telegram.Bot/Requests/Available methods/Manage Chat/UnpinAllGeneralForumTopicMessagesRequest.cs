namespace Telegram.Bot.Requests;

/// <summary>Use this method to clear the list of pinned messages in a General forum topic. The bot must be an administrator in the chat for this to work and must have the <em>CanPinMessages</em> administrator right in the supergroup.<para>Returns: </para></summary>
public partial class UnpinAllGeneralForumTopicMessagesRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Initializes an instance of <see cref="UnpinAllGeneralForumTopicMessagesRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public UnpinAllGeneralForumTopicMessagesRequest(ChatId chatId) : this() => ChatId = chatId;

    /// <summary>Instantiates a new <see cref="UnpinAllGeneralForumTopicMessagesRequest"/></summary>
    public UnpinAllGeneralForumTopicMessagesRequest() : base("unpinAllGeneralForumTopicMessages") { }
}
