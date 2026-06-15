// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Represents a sticker file to be sent.</summary>
public partial class InputMediaSticker : InputPollOptionMedia
{
    /// <summary>Type of the media, must be <em>sticker</em></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public InputMediaType Type => InputMediaType.Sticker;

    /// <summary>File to send. Pass a FileId to send a file that exists on the Telegram servers (recommended), pass an HTTP URL for Telegram to get a .WEBP sticker from the Internet, or pass “attach://&lt;FileAttachName&gt;” to upload a new .WEBP, .TGS, or .WEBM sticker using <see cref="InputFileStream"/> under &lt;FileAttachName&gt; name. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFile Media { get; set; }

    /// <summary><em>Optional</em>. Emoji associated with the sticker; only for just uploaded stickers</summary>
    public string? Emoji { get; set; }

    /// <summary>Initializes an instance of <see cref="InputMediaSticker"/></summary>
    /// <param name="media">File to send. Pass a FileId to send a file that exists on the Telegram servers (recommended), pass an HTTP URL for Telegram to get a .WEBP sticker from the Internet, or pass “attach://&lt;FileAttachName&gt;” to upload a new .WEBP, .TGS, or .WEBM sticker using <see cref="InputFileStream"/> under &lt;FileAttachName&gt; name. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    [SetsRequiredMembers]
    public InputMediaSticker(InputFile media) => Media = media;

    /// <summary>Instantiates a new <see cref="InputMediaSticker"/></summary>
    public InputMediaSticker() { }
}
