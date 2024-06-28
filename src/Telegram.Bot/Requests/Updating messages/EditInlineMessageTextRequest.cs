namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit text and <a href="https://core.telegram.org/bots/api#games">game</a> messages.<para>Returns: </para></summary>
public partial class EditInlineMessageTextRequest : RequestBase<bool>, IBusinessConnectable
{
    /// <summary>Identifier of the inline message</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string InlineMessageId { get; set; }

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

    /// <summary>Initializes an instance of <see cref="EditInlineMessageTextRequest"/></summary>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="text">New text of the message, 1-4096 characters after entities parsing</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public EditInlineMessageTextRequest(string inlineMessageId, string text) : this()
    {
        InlineMessageId = inlineMessageId;
        Text = text;
    }

    /// <summary>Instantiates a new <see cref="EditInlineMessageTextRequest"/></summary>
    public EditInlineMessageTextRequest() : base("editMessageText") { }
}
