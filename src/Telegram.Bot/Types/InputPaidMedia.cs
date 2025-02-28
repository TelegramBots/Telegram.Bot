// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes the paid media to be sent. Currently, it can be one of<br/><see cref="InputPaidMediaPhoto"/>, <see cref="InputPaidMediaVideo"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<InputPaidMedia>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(InputPaidMediaPhoto), "photo")]
[CustomJsonDerivedType(typeof(InputPaidMediaVideo), "video")]
public abstract partial class InputPaidMedia
{
    /// <summary>Type of the media</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract InputPaidMediaType Type { get; }

    /// <summary>File to send. Pass a FileId to send a file that exists on the Telegram servers (recommended), pass an HTTP URL for Telegram to get a file from the Internet, or use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public InputFile Media { get; set; } = default!;
}

/// <summary>The paid media to send is a photo.</summary>
public partial class InputPaidMediaPhoto : InputPaidMedia
{
    /// <summary>Type of the media, always <see cref="InputPaidMediaType.Photo"/></summary>
    public override InputPaidMediaType Type => InputPaidMediaType.Photo;
}

/// <summary>The paid media to send is a video.</summary>
public partial class InputPaidMediaVideo : InputPaidMedia, IInputMediaThumb
{
    /// <summary>Type of the media, always <see cref="InputPaidMediaType.Video"/></summary>
    public override InputPaidMediaType Type => InputPaidMediaType.Video;

    /// <summary><em>Optional</em>. Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side. The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should not exceed 320. Ignored if the file is not uploaded using <see cref="InputFileStream"/>. Thumbnails can't be reused and can be only uploaded as a new file, so you can use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    public InputFile? Thumbnail { get; set; }

    /// <summary><em>Optional</em>. Cover for the video in the message. Pass a FileId to send a file that exists on the Telegram servers (recommended), pass an HTTP URL for Telegram to get a file from the Internet, or use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    public InputFile? Cover { get; set; }

    /// <summary><em>Optional</em>. Start timestamp for the video in the message</summary>
    [JsonPropertyName("start_timestamp")]
    public int? StartTimestamp { get; set; }

    /// <summary><em>Optional</em>. Video width</summary>
    public int Width { get; set; }

    /// <summary><em>Optional</em>. Video height</summary>
    public int Height { get; set; }

    /// <summary><em>Optional</em>. Video duration in seconds</summary>
    public int Duration { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the uploaded video is suitable for streaming</summary>
    [JsonPropertyName("supports_streaming")]
    public bool SupportsStreaming { get; set; }
}
