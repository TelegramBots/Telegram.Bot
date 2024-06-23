namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit animation, audio, document, photo, or video messages. If a message is part of a message album, then it can be edited only to an audio for audio albums, only to a document for document albums and to a photo or a video otherwise. When an inline message is edited, a new file can't be uploaded; use a previously uploaded file via its FileId or specify a URL.<para>Returns: The edited <see cref="Message"/> is returned</para></summary>
public partial class EditMessageMediaRequest : FileRequestBase<Message>, IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of the message to edit</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>An object for a new media content of the message</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputMedia Media { get; set; }

    /// <summary>An object for a new <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</summary>
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message to be edited was sent</summary>
    public string? BusinessConnectionId { get; set; }

    /// <summary>Initializes an instance of <see cref="EditMessageMediaRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of the message to edit</param>
    /// <param name="media">An object for a new media content of the message</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public EditMessageMediaRequest(ChatId chatId, int messageId, InputMedia media) : this()
    {
        ChatId = chatId;
        MessageId = messageId;
        Media = media;
    }

    /// <summary>Instantiates a new <see cref="EditMessageMediaRequest"/></summary>
    public EditMessageMediaRequest() : base("editMessageMedia") { }
}
