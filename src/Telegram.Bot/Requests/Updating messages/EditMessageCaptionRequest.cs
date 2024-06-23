namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit captions of messages.<para>Returns: The edited <see cref="Message"/> is returned</para></summary>
public partial class EditMessageCaptionRequest : RequestBase<Message>, IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of the message to edit</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>New caption of the message, 0-1024 characters after entities parsing</summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Caption { get; set; }

    /// <summary>Mode for parsing entities in the message caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ParseMode ParseMode { get; set; }

    /// <summary>A list of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<MessageEntity>? CaptionEntities { get; set; }

    /// <summary>Pass <see langword="true"/>, if the caption must be shown above the message media. Supported only for animation, photo and video messages.</summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary>An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message to be edited was sent</summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BusinessConnectionId { get; set; }

    /// <summary>Initializes an instance of <see cref="EditMessageCaptionRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of the message to edit</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public EditMessageCaptionRequest(ChatId chatId, int messageId)
        : this()
    {
        ChatId = chatId;
        MessageId = messageId;
    }

    /// <summary>Instantiates a new <see cref="EditMessageCaptionRequest"/></summary>
    public EditMessageCaptionRequest()
        : base("editMessageCaption")
    { }
}
