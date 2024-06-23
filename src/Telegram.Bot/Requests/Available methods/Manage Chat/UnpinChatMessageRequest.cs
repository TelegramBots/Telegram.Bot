namespace Telegram.Bot.Requests;

/// <summary>Use this method to remove a message from the list of pinned messages in a chat. If the chat is not a private chat, the bot must be an administrator in the chat for this to work and must have the 'CanPinMessages' administrator right in a supergroup or 'CanEditMessages' administrator right in a channel.<para>Returns: </para></summary>
public partial class UnpinChatMessageRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of a message to unpin. If not specified, the most recent pinned message (by sending date) will be unpinned.</summary>
    public int? MessageId { get; set; }

    /// <summary>Initializes an instance of <see cref="UnpinChatMessageRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public UnpinChatMessageRequest(ChatId chatId) : this() => ChatId = chatId;

    /// <summary>Instantiates a new <see cref="UnpinChatMessageRequest"/></summary>
    public UnpinChatMessageRequest() : base("unpinChatMessage") { }
}
