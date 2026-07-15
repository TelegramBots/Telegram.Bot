// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Represents a voice message file to be sent.</summary>
public partial class InputMediaVoiceNote : IInputRichMedia
{
    /// <summary>Type of the media, must be <em>VoiceNote</em></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public InputMediaType Type => InputMediaType.VoiceNote;

    /// <summary>File to send. Pass a FileId to send a file that exists on the Telegram servers (recommended), pass an HTTP URL for Telegram to get a file from the Internet, or pass "attach://&lt;FileAttachName&gt;" to upload a new one using <see cref="InputFileStream"/> under &lt;FileAttachName&gt; name. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFile Media { get; set; }

    /// <summary><em>Optional</em>. Caption of the voice message to be sent, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the voice message caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary><em>Optional</em>. Duration of the voice message in seconds</summary>
    public int Duration { get; set; }

    /// <summary>Initializes an instance of <see cref="InputMediaVoiceNote"/></summary>
    /// <param name="media">File to send. Pass a FileId to send a file that exists on the Telegram servers (recommended), pass an HTTP URL for Telegram to get a file from the Internet, or pass "attach://&lt;FileAttachName&gt;" to upload a new one using <see cref="InputFileStream"/> under &lt;FileAttachName&gt; name. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    [SetsRequiredMembers]
    public InputMediaVoiceNote(InputFile media) => Media = media;

    /// <summary>Instantiates a new <see cref="InputMediaVoiceNote"/></summary>
    public InputMediaVoiceNote() { }
}
