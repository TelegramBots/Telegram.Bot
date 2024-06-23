namespace Telegram.Bot.Requests;

/// <summary>Use this method to send static .WEBP, <a href="https://telegram.org/blog/animated-stickers">animated</a> .TGS, or <a href="https://telegram.org/blog/video-stickers-better-reactions">video</a> .WEBM stickers.<para>Returns: The sent <see cref="Message"/> is returned.</para></summary>
public partial class SendStickerRequest : FileRequestBase<Message>, IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Sticker to send. Pass a FileId as String to send a file that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get a .WEBP sticker from the Internet, or upload a new .WEBP, .TGS, or .WEBM sticker using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a>. Video and animated stickers can't be sent via an HTTP URL.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFile Sticker { get; set; }

    /// <summary>Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</summary>
    public int? MessageThreadId { get; set; }

    /// <summary>Emoji associated with the sticker; only for just uploaded stickers</summary>
    public string? Emoji { get; set; }

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

    /// <summary>Initializes an instance of <see cref="SendStickerRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="sticker">Sticker to send. Pass a FileId as String to send a file that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get a .WEBP sticker from the Internet, or upload a new .WEBP, .TGS, or .WEBM sticker using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a>. Video and animated stickers can't be sent via an HTTP URL.</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SendStickerRequest(ChatId chatId, InputFile sticker) : this()
    {
        ChatId = chatId;
        Sticker = sticker;
    }

    /// <summary>Instantiates a new <see cref="SendStickerRequest"/></summary>
    public SendStickerRequest() : base("sendSticker") { }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
        => Sticker is InputFileStream ifs ? ToMultipartFormDataContent("sticker", ifs) : base.ToHttpContent();
}
