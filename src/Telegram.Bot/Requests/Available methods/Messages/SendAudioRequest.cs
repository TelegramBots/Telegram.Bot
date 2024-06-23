namespace Telegram.Bot.Requests;

/// <summary>Use this method to send audio files, if you want Telegram clients to display them in the music player. Your audio must be in the .MP3 or .M4A format.<para>Returns: The sent <see cref="Message"/> is returned.</para></summary>
/// <remarks>Bots can currently send audio files of up to 50 MB in size, this limit may be changed in the future.<br/>For sending voice messages, use the <see cref="TelegramBotClientExtensions.SendVoiceAsync">SendVoice</see> method instead.</remarks>
public partial class SendAudioRequest : FileRequestBase<Message>, IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Audio file to send. Pass a FileId as String to send an audio file that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get an audio file from the Internet, or upload a new one using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFile Audio { get; set; }

    /// <summary>Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</summary>
    public int? MessageThreadId { get; set; }

    /// <summary>Audio caption, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary>Mode for parsing entities in the audio caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    public ParseMode ParseMode { get; set; }

    /// <summary>A list of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    public IEnumerable<MessageEntity>? CaptionEntities { get; set; }

    /// <summary>Duration of the audio in seconds</summary>
    public int? Duration { get; set; }

    /// <summary>Performer</summary>
    public string? Performer { get; set; }

    /// <summary>Track name</summary>
    public string? Title { get; set; }

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

    /// <summary>Initializes an instance of <see cref="SendAudioRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="audio">Audio file to send. Pass a FileId as String to send an audio file that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get an audio file from the Internet, or upload a new one using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SendAudioRequest(ChatId chatId, InputFile audio) : this()
    {
        ChatId = chatId;
        Audio = audio;
    }

    /// <summary>Instantiates a new <see cref="SendAudioRequest"/></summary>
    public SendAudioRequest() : base("sendAudio") { }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
        => Audio is InputFileStream || Thumbnail is InputFileStream
            ? GenerateMultipartFormDataContent("audio", "thumbnail")
                .AddContentIfInputFile(media: Audio, name: "audio")
                .AddContentIfInputFile(media: Thumbnail, name: "thumbnail")
            : base.ToHttpContent();
}
