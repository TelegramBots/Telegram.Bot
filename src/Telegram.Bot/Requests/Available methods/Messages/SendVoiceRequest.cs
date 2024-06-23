namespace Telegram.Bot.Requests;

/// <summary>Use this method to send audio files, if you want Telegram clients to display the file as a playable voice message. For this to work, your audio must be in an .OGG file encoded with OPUS, or in .MP3 format, or in .M4A format (other formats may be sent as <see cref="Audio"/> or <see cref="Document"/>).<para>Returns: The sent <see cref="Message"/> is returned.</para></summary>
/// <remarks>Bots can currently send voice messages of up to 50 MB in size, this limit may be changed in the future.</remarks>
public partial class SendVoiceRequest : FileRequestBase<Message>, IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Audio file to send. Pass a FileId as String to send a file that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get a file from the Internet, or upload a new one using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFile Voice { get; set; }

    /// <summary>Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</summary>
    public int? MessageThreadId { get; set; }

    /// <summary>Voice message caption, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary>Mode for parsing entities in the voice message caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    public ParseMode ParseMode { get; set; }

    /// <summary>A list of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    public IEnumerable<MessageEntity>? CaptionEntities { get; set; }

    /// <summary>Duration of the voice message in seconds</summary>
    public int? Duration { get; set; }

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

    /// <summary>Initializes an instance of <see cref="SendVoiceRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="voice">Audio file to send. Pass a FileId as String to send a file that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get a file from the Internet, or upload a new one using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SendVoiceRequest(ChatId chatId, InputFile voice) : this()
    {
        ChatId = chatId;
        Voice = voice;
    }

    /// <summary>Instantiates a new <see cref="SendVoiceRequest"/></summary>
    public SendVoiceRequest() : base("sendVoice") { }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
        => Voice is InputFileStream ifs ? ToMultipartFormDataContent("voice", ifs) : base.ToHttpContent();
}
