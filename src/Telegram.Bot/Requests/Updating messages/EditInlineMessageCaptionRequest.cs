namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit captions of messages.<para>Returns: </para></summary>
public partial class EditInlineMessageCaptionRequest : RequestBase<bool>, IBusinessConnectable
{
    /// <summary>Identifier of the inline message</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string InlineMessageId { get; set; }

    /// <summary>New caption of the message, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary>Mode for parsing entities in the message caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    public ParseMode ParseMode { get; set; }

    /// <summary>A list of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    public IEnumerable<MessageEntity>? CaptionEntities { get; set; }

    /// <summary>Pass <see langword="true"/>, if the caption must be shown above the message media. Supported only for animation, photo and video messages.</summary>
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary>An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</summary>
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message to be edited was sent</summary>
    public string? BusinessConnectionId { get; set; }

    /// <summary>Initializes an instance of <see cref="EditInlineMessageCaptionRequest"/></summary>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public EditInlineMessageCaptionRequest(string inlineMessageId) : this() => InlineMessageId = inlineMessageId;

    /// <summary>Instantiates a new <see cref="EditInlineMessageCaptionRequest"/></summary>
    public EditInlineMessageCaptionRequest() : base("editMessageCaption") { }
}
