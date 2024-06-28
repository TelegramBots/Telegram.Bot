namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit text and <a href="https://core.telegram.org/bots/api#games">game</a> messages.<para>Returns: The edited <see cref="Message"/> is returned</para></summary>
public partial class EditMessageTextRequest : RequestBase<Message>, IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of the message to edit</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>New text of the message, 1-4096 characters after entities parsing</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Text { get; set; }

    /// <summary>Mode for parsing entities in the message text. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    public ParseMode ParseMode { get; set; }

    /// <summary>A list of special entities that appear in message text, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    public IEnumerable<MessageEntity>? Entities { get; set; }

    /// <summary>Link preview generation options for the message</summary>
    public LinkPreviewOptions? LinkPreviewOptions { get; set; }

    /// <summary>An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</summary>
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message to be edited was sent</summary>
    public string? BusinessConnectionId { get; set; }

    /// <summary>Initializes an instance of <see cref="EditMessageTextRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of the message to edit</param>
    /// <param name="text">New text of the message, 1-4096 characters after entities parsing</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public EditMessageTextRequest(ChatId chatId, int messageId, string text) : this()
    {
        ChatId = chatId;
        MessageId = messageId;
        Text = text;
    }

    /// <summary>Instantiates a new <see cref="EditMessageTextRequest"/></summary>
    public EditMessageTextRequest() : base("editMessageText") { }
}
