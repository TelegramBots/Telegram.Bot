namespace Telegram.Bot.Requests;

/// <summary>Use this method to forward messages of any kind. Service messages and messages with protected content can't be forwarded.<para>Returns: The sent <see cref="Message"/> is returned.</para></summary>
public partial class ForwardMessageRequest : RequestBase<Message>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier for the chat where the original message was sent (or channel username in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId FromChatId { get; set; }

    /// <summary>Message identifier in the chat specified in <see cref="FromChatId">FromChatId</see></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</summary>
    public int? MessageThreadId { get; set; }

    /// <summary>Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</summary>
    public bool DisableNotification { get; set; }

    /// <summary>Protects the contents of the forwarded message from forwarding and saving</summary>
    public bool ProtectContent { get; set; }

    /// <summary>Initializes an instance of <see cref="ForwardMessageRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="fromChatId">Unique identifier for the chat where the original message was sent (or channel username in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Message identifier in the chat specified in <see cref="FromChatId">FromChatId</see></param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public ForwardMessageRequest(ChatId chatId, ChatId fromChatId, int messageId) : this()
    {
        ChatId = chatId;
        FromChatId = fromChatId;
        MessageId = messageId;
    }

    /// <summary>Instantiates a new <see cref="ForwardMessageRequest"/></summary>
    public ForwardMessageRequest() : base("forwardMessage") { }
}
