namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit only the reply markup of messages.<para>Returns: </para></summary>
public partial class EditInlineMessageReplyMarkupRequest : RequestBase<bool>, IBusinessConnectable
{
    /// <summary>Identifier of the inline message</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string InlineMessageId { get; set; }

    /// <summary>An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</summary>
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message to be edited was sent</summary>
    public string? BusinessConnectionId { get; set; }

    /// <summary>Initializes an instance of <see cref="EditInlineMessageReplyMarkupRequest"/></summary>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public EditInlineMessageReplyMarkupRequest(string inlineMessageId) : this() => InlineMessageId = inlineMessageId;

    /// <summary>Instantiates a new <see cref="EditInlineMessageReplyMarkupRequest"/></summary>
    public EditInlineMessageReplyMarkupRequest() : base("editMessageReplyMarkup") { }
}
