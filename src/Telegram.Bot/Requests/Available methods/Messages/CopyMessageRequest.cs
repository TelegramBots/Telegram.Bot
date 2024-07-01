namespace Telegram.Bot.Requests;

/// <summary>Use this method to copy messages of any kind. Service messages, paid media messages, giveaway messages, giveaway winners messages, and invoice messages can't be copied. A quiz <see cref="Poll"/> can be copied only if the value of the field <em>CorrectOptionId</em> is known to the bot. The method is analogous to the method <see cref="TelegramBotClientExtensions.ForwardMessageAsync">ForwardMessage</see>, but the copied message doesn't have a link to the original message.<para>Returns: The <see cref="MessageId"/> of the sent message on success.</para></summary>
public partial class CopyMessageRequest : RequestBase<MessageId>, IChatTargetable
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

    /// <summary>New caption for media, 0-1024 characters after entities parsing. If not specified, the original caption is kept</summary>
    public string? Caption { get; set; }

    /// <summary>Mode for parsing entities in the new caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    public ParseMode ParseMode { get; set; }

    /// <summary>A list of special entities that appear in the new caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    public IEnumerable<MessageEntity>? CaptionEntities { get; set; }

    /// <summary>Pass <see langword="true"/>, if the caption must be shown above the message media. Ignored if a new caption isn't specified.</summary>
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary>Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</summary>
    public bool DisableNotification { get; set; }

    /// <summary>Protects the contents of the sent message from forwarding and saving</summary>
    public bool ProtectContent { get; set; }

    /// <summary>Description of the message to reply to</summary>
    public ReplyParameters? ReplyParameters { get; set; }

    /// <summary>Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</summary>
    public IReplyMarkup? ReplyMarkup { get; set; }

    /// <summary>Initializes an instance of <see cref="CopyMessageRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="fromChatId">Unique identifier for the chat where the original message was sent (or channel username in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Message identifier in the chat specified in <see cref="FromChatId">FromChatId</see></param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public CopyMessageRequest(ChatId chatId, ChatId fromChatId, int messageId) : this()
    {
        ChatId = chatId;
        FromChatId = fromChatId;
        MessageId = messageId;
    }

    /// <summary>Instantiates a new <see cref="CopyMessageRequest"/></summary>
    public CopyMessageRequest() : base("copyMessage") { }
}
