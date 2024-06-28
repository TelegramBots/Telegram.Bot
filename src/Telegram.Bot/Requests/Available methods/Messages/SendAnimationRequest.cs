namespace Telegram.Bot.Requests;

/// <summary>Use this method to send animation files (GIF or H.264/MPEG-4 AVC video without sound).<para>Returns: The sent <see cref="Message"/> is returned.</para></summary>
/// <remarks>Bots can currently send animation files of up to 50 MB in size, this limit may be changed in the future.</remarks>
public partial class SendAnimationRequest : FileRequestBase<Message>, IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Animation to send. Pass a FileId as String to send an animation that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get an animation from the Internet, or upload a new animation using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFile Animation { get; set; }

    /// <summary>Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</summary>
    public int? MessageThreadId { get; set; }

    /// <summary>Duration of sent animation in seconds</summary>
    public int? Duration { get; set; }

    /// <summary>Animation width</summary>
    public int? Width { get; set; }

    /// <summary>Animation height</summary>
    public int? Height { get; set; }

    /// <summary>Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side. The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should not exceed 320. Ignored if the file is not uploaded using <see cref="InputFileStream"/>. Thumbnails can't be reused and can be only uploaded as a new file, so you can pass “attach://&lt;FileAttachName&gt;” if the thumbnail was uploaded using <see cref="InputFileStream"/> under &lt;FileAttachName&gt;. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    public InputFile? Thumbnail { get; set; }

    /// <summary>Animation caption (may also be used when resending animation by <em>FileId</em>), 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary>Mode for parsing entities in the animation caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    public ParseMode ParseMode { get; set; }

    /// <summary>A list of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    public IEnumerable<MessageEntity>? CaptionEntities { get; set; }

    /// <summary>Pass <see langword="true"/>, if the caption must be shown above the message media</summary>
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary>Pass <see langword="true"/> if the animation needs to be covered with a spoiler animation</summary>
    public bool HasSpoiler { get; set; }

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

    /// <summary>Initializes an instance of <see cref="SendAnimationRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="animation">Animation to send. Pass a FileId as String to send an animation that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get an animation from the Internet, or upload a new animation using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SendAnimationRequest(ChatId chatId, InputFile animation) : this()
    {
        ChatId = chatId;
        Animation = animation;
    }

    /// <summary>Instantiates a new <see cref="SendAnimationRequest"/></summary>
    public SendAnimationRequest() : base("sendAnimation") { }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
        => Animation is InputFileStream || Thumbnail is InputFileStream
            ? GenerateMultipartFormDataContent("animation", "thumbnail")
                .AddContentIfInputFile(media: Animation, name: "animation")
                .AddContentIfInputFile(media: Thumbnail, name: "thumbnail")
            : base.ToHttpContent();
}
