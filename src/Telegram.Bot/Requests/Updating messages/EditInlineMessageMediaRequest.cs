namespace Telegram.Bot.Requests;

/// <summary>Use this method to edit animation, audio, document, photo, or video messages, or to add media to text messages. If a message is part of a message album, then it can be edited only to an audio for audio albums, only to a document for document albums and to a photo or a video otherwise. When an inline message is edited, a new file can't be uploaded; use a previously uploaded file via its FileId or specify a URL.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class EditInlineMessageMediaRequest() : FileRequestBase<bool>("editMessageMedia"), IBusinessConnectable
{
    /// <summary>Identifier of the inline message</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string InlineMessageId { get; set; }

    /// <summary>An object for a new media content of the message</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputMedia Media { get; set; }

    /// <summary>An object for a new <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</summary>
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message to be edited was sent</summary>
    public string? BusinessConnectionId { get; set; }
}
