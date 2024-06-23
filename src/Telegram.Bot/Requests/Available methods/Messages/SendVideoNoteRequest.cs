namespace Telegram.Bot.Requests;

/// <summary>As of <a href="https://telegram.org/blog/video-messages-and-telescope">v.4.0</a>, Telegram clients support rounded square MPEG4 videos of up to 1 minute long. Use this method to send video messages.<para>Returns: The sent <see cref="Message"/> is returned.</para></summary>
public partial class SendVideoNoteRequest : FileRequestBase<Message>, IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Video note to send. Pass a FileId as String to send a video note that exists on the Telegram servers (recommended) or upload a new video using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a>. Sending video notes by a URL is currently unsupported</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFile VideoNote { get; set; }

    /// <summary>Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</summary>
    public int? MessageThreadId { get; set; }

    /// <summary>Duration of sent video in seconds</summary>
    public int? Duration { get; set; }

    /// <summary>Video width and height, i.e. diameter of the video message</summary>
    public int? Length { get; set; }

    /// <summary>Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side. The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should not exceed 320. Ignored if the file is not uploaded using <see cref="InputFileStream"/>. Thumbnails can't be reused and can be only uploaded as a new file, so you can pass “attach://&lt;FileAttachName&gt;” if the thumbnail was uploaded using <see cref="InputFileStream"/> under &lt;FileAttachName&gt;. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    public InputFile? Thumbnail { get; set; }

    /// <summary>Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</summary>
    public bool DisableNotification { get; set; }

    /// <summary>Protects the contents of the sent message from forwarding and saving</summary>
    public bool ProtectContent { get; set; }

    /// <summary>Unique identifier of the message effect to be added to the message; for private chats only</summary>
    public string? MessageEffectId { get; set; }

    /// <summary>Description of the message to reply to</summary>
    public ReplyParameters? ReplyParameters { get; set; }

    /// <summary>Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</summary>
    public IReplyMarkup? ReplyMarkup { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message will be sent</summary>
    public string? BusinessConnectionId { get; set; }

    /// <summary>Initializes an instance of <see cref="SendVideoNoteRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="videoNote">Video note to send. Pass a FileId as String to send a video note that exists on the Telegram servers (recommended) or upload a new video using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a>. Sending video notes by a URL is currently unsupported</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SendVideoNoteRequest(ChatId chatId, InputFile videoNote) : this()
    {
        ChatId = chatId;
        VideoNote = videoNote;
    }

    /// <summary>Instantiates a new <see cref="SendVideoNoteRequest"/></summary>
    public SendVideoNoteRequest() : base("sendVideoNote") { }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
        => VideoNote is InputFileStream || Thumbnail is InputFileStream
            ? GenerateMultipartFormDataContent("video_note", "thumbnail")
                .AddContentIfInputFile(media: VideoNote, name: "video_note")
                .AddContentIfInputFile(media: Thumbnail, name: "thumbnail")
            : base.ToHttpContent();
}
