// This file is NOT auto-generated - check https://core.telegram.org/bots/api#inputrichmessagemedia
namespace Telegram.Bot.Types;

/// <summary>A marker for input media types that can be used as <see cref="InputRichMessageMedia.Media">Media</see> for the <see cref="InputRichMessage.Media"/> entries.</summary>
[JsonConverter(typeof(PolymorphicJsonConverter<IInputRichMedia>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(InputMediaAnimation), "animation")]
[CustomJsonDerivedType(typeof(InputMediaAudio), "audio")]
[CustomJsonDerivedType(typeof(InputMediaDocument), "document")]
[CustomJsonDerivedType(typeof(InputMediaPhoto), "photo")]
[CustomJsonDerivedType(typeof(InputMediaVideo), "video")]
[CustomJsonDerivedType(typeof(InputMediaVoiceNote), "voice_note")]
public interface IInputRichMedia
{
    /// <summary>Type of the media</summary>
    InputMediaType Type { get; }

    /// <summary>File to send. Pass a FileId to send a file that exists on the Telegram servers (recommended), pass an HTTP URL for Telegram to get a file from the Internet, or use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    InputFile Media { get; }

    /// <summary><em>Optional</em>. Caption of the InputMedia to be sent, 0-1024 characters after entities parsing</summary>
    string? Caption { get; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the InputMedia caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    ParseMode ParseMode { get; }

    /// <summary><em>Optional</em>. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    MessageEntity[]? CaptionEntities { get; }
}
