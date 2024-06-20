namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to send text messages.<para>Returns: The sent <see cref="Message"/> is returned.</para>
/// </summary>
public partial class SendMessageRequest : RequestBase<Message>, IChatTargetable, IBusinessConnectable
{
    /// <summary>
    /// Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>
    /// Text of the message to be sent, 1-4096 characters after entities parsing
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Text { get; set; }

    /// <summary>
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MessageThreadId { get; set; }

    /// <summary>
    /// Mode for parsing entities in the message text. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ParseMode ParseMode { get; set; }

    /// <summary>
    /// A list of special entities that appear in message text, which can be specified instead of <see cref="ParseMode">ParseMode</see>
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<MessageEntity>? Entities { get; set; }

    /// <summary>
    /// Link preview generation options for the message
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public LinkPreviewOptions? LinkPreviewOptions { get; set; }

    /// <summary>
    /// Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool DisableNotification { get; set; }

    /// <summary>
    /// Protects the contents of the sent message from forwarding and saving
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool ProtectContent { get; set; }

    /// <summary>
    /// Unique identifier of the message effect to be added to the message; for private chats only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? MessageEffectId { get; set; }

    /// <summary>
    /// Description of the message to reply to
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ReplyParameters? ReplyParameters { get; set; }

    /// <summary>
    /// Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReplyMarkup? ReplyMarkup { get; set; }

    /// <summary>
    /// Unique identifier of the business connection on behalf of which the message will be sent
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BusinessConnectionId { get; set; }

    /// <summary>
    /// Initializes an instance of <see cref="SendMessageRequest"/>
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="text">Text of the message to be sent, 1-4096 characters after entities parsing</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SendMessageRequest(ChatId chatId, string text)
        : this()
    {
        ChatId = chatId;
        Text = text;
    }

    /// <summary>
    /// Instantiates a new <see cref="SendMessageRequest"/>
    /// </summary>
    public SendMessageRequest()
        : base("sendMessage")
    { }
}
