namespace Telegram.Bot.Requests;

/// <summary>Use this method to forward multiple messages of any kind. If some of the specified messages can't be found or forwarded, they are skipped. Service messages and messages with protected content can't be forwarded. Album grouping is kept for forwarded messages.<para>Returns: An array of <see cref="MessageId"/> of the sent messages is returned.</para></summary>
public partial class ForwardMessagesRequest : RequestBase<MessageId[]>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier for the chat where the original messages were sent (or channel username in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId FromChatId { get; set; }

    /// <summary>A list of 1-100 identifiers of messages in the chat <see cref="FromChatId">FromChatId</see> to forward. The identifiers must be specified in a strictly increasing order.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<int> MessageIds { get; set; }

    /// <summary>Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</summary>
    public int? MessageThreadId { get; set; }

    /// <summary>Sends the messages <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</summary>
    public bool DisableNotification { get; set; }

    /// <summary>Protects the contents of the forwarded messages from forwarding and saving</summary>
    public bool ProtectContent { get; set; }

    /// <summary>Initializes an instance of <see cref="ForwardMessagesRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="fromChatId">Unique identifier for the chat where the original messages were sent (or channel username in the format <c>@channelusername</c>)</param>
    /// <param name="messageIds">A list of 1-100 identifiers of messages in the chat <see cref="FromChatId">FromChatId</see> to forward. The identifiers must be specified in a strictly increasing order.</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public ForwardMessagesRequest(ChatId chatId, ChatId fromChatId, IEnumerable<int> messageIds) : this()
    {
        ChatId = chatId;
        FromChatId = fromChatId;
        MessageIds = messageIds;
    }

    /// <summary>Instantiates a new <see cref="ForwardMessagesRequest"/></summary>
    public ForwardMessagesRequest() : base("forwardMessages") { }
}
