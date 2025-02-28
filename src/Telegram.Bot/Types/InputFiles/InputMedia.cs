// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents the content of a media message to be sent. It should be one of<br/><see cref="InputMediaAnimation"/>, <see cref="InputMediaDocument"/>, <see cref="InputMediaAudio"/>, <see cref="InputMediaPhoto"/>, <see cref="InputMediaVideo"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<InputMedia>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(InputMediaAnimation), "animation")]
[CustomJsonDerivedType(typeof(InputMediaDocument), "document")]
[CustomJsonDerivedType(typeof(InputMediaAudio), "audio")]
[CustomJsonDerivedType(typeof(InputMediaPhoto), "photo")]
[CustomJsonDerivedType(typeof(InputMediaVideo), "video")]
public abstract partial class InputMedia
{
    /// <summary>Type of the result</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract InputMediaType Type { get; }

    /// <summary>File to send. Pass a FileId to send a file that exists on the Telegram servers (recommended), pass an HTTP URL for Telegram to get a file from the Internet, or use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFile Media { get; set; }

    /// <summary><em>Optional</em>. Caption of the InputMedia to be sent, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary><em>Optional</em>. Mode for parsing entities in the InputMedia caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary><em>Optional</em>. List of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary>Initializes an instance of <see cref="InputMedia"/></summary>
    /// <param name="media">File to send. Pass a FileId to send a file that exists on the Telegram servers (recommended), pass an HTTP URL for Telegram to get a file from the Internet, or use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    [SetsRequiredMembers]
    protected InputMedia(InputFile media) => Media = media;

    /// <summary>Instantiates a new <see cref="InputMedia"/></summary>
    protected InputMedia() { }
}

/// <summary>Represents a photo to be sent.</summary>
public partial class InputMediaPhoto : InputMedia, IAlbumInputMedia
{
    /// <summary>Type of the result, always <see cref="InputMediaType.Photo"/></summary>
    public override InputMediaType Type => InputMediaType.Photo;

    /// <summary><em>Optional</em>. Pass <see langword="true"/>, if the caption must be shown above the message media</summary>
    [JsonPropertyName("show_caption_above_media")]
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the photo needs to be covered with a spoiler animation</summary>
    [JsonPropertyName("has_spoiler")]
    public bool HasSpoiler { get; set; }

    /// <summary>Initializes an instance of <see cref="InputMediaPhoto"/></summary>
    /// <param name="media">File to send. Pass a FileId to send a file that exists on the Telegram servers (recommended), pass an HTTP URL for Telegram to get a file from the Internet, or use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    [SetsRequiredMembers]
    public InputMediaPhoto(InputFile media) : base(media) { }

    /// <summary>Instantiates a new <see cref="InputMediaPhoto"/></summary>
    public InputMediaPhoto() { }
}

/// <summary>Represents a video to be sent.</summary>
public partial class InputMediaVideo : InputMedia, IInputMediaThumb, IAlbumInputMedia
{
    /// <summary>Type of the result, always <see cref="InputMediaType.Video"/></summary>
    public override InputMediaType Type => InputMediaType.Video;

    /// <summary><em>Optional</em>. Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side. The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should not exceed 320. Ignored if the file is not uploaded using <see cref="InputFileStream"/>. Thumbnails can't be reused and can be only uploaded as a new file, so you can use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    public InputFile? Thumbnail { get; set; }

    /// <summary><em>Optional</em>. Cover for the video in the message. Pass a FileId to send a file that exists on the Telegram servers (recommended), pass an HTTP URL for Telegram to get a file from the Internet, or use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    public InputFile? Cover { get; set; }

    /// <summary><em>Optional</em>. Start timestamp for the video in the message</summary>
    [JsonPropertyName("start_timestamp")]
    public int? StartTimestamp { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/>, if the caption must be shown above the message media</summary>
    [JsonPropertyName("show_caption_above_media")]
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary><em>Optional</em>. Video width</summary>
    public int Width { get; set; }

    /// <summary><em>Optional</em>. Video height</summary>
    public int Height { get; set; }

    /// <summary><em>Optional</em>. Video duration in seconds</summary>
    public int Duration { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the uploaded video is suitable for streaming</summary>
    [JsonPropertyName("supports_streaming")]
    public bool SupportsStreaming { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the video needs to be covered with a spoiler animation</summary>
    [JsonPropertyName("has_spoiler")]
    public bool HasSpoiler { get; set; }

    /// <summary>Initializes an instance of <see cref="InputMediaVideo"/></summary>
    /// <param name="media">File to send. Pass a FileId to send a file that exists on the Telegram servers (recommended), pass an HTTP URL for Telegram to get a file from the Internet, or use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    [SetsRequiredMembers]
    public InputMediaVideo(InputFile media) : base(media) { }

    /// <summary>Instantiates a new <see cref="InputMediaVideo"/></summary>
    public InputMediaVideo() { }
}

/// <summary>Represents an animation file (GIF or H.264/MPEG-4 AVC video without sound) to be sent.</summary>
public partial class InputMediaAnimation : InputMedia, IInputMediaThumb
{
    /// <summary>Type of the result, always <see cref="InputMediaType.Animation"/></summary>
    public override InputMediaType Type => InputMediaType.Animation;

    /// <summary><em>Optional</em>. Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side. The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should not exceed 320. Ignored if the file is not uploaded using <see cref="InputFileStream"/>. Thumbnails can't be reused and can be only uploaded as a new file, so you can use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    public InputFile? Thumbnail { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/>, if the caption must be shown above the message media</summary>
    [JsonPropertyName("show_caption_above_media")]
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary><em>Optional</em>. Animation width</summary>
    public int Width { get; set; }

    /// <summary><em>Optional</em>. Animation height</summary>
    public int Height { get; set; }

    /// <summary><em>Optional</em>. Animation duration in seconds</summary>
    public int Duration { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the animation needs to be covered with a spoiler animation</summary>
    [JsonPropertyName("has_spoiler")]
    public bool HasSpoiler { get; set; }

    /// <summary>Initializes an instance of <see cref="InputMediaAnimation"/></summary>
    /// <param name="media">File to send. Pass a FileId to send a file that exists on the Telegram servers (recommended), pass an HTTP URL for Telegram to get a file from the Internet, or use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    [SetsRequiredMembers]
    public InputMediaAnimation(InputFile media) : base(media) { }

    /// <summary>Instantiates a new <see cref="InputMediaAnimation"/></summary>
    public InputMediaAnimation() { }
}

/// <summary>Represents an audio file to be treated as music to be sent.</summary>
public partial class InputMediaAudio : InputMedia, IInputMediaThumb, IAlbumInputMedia
{
    /// <summary>Type of the result, always <see cref="InputMediaType.Audio"/></summary>
    public override InputMediaType Type => InputMediaType.Audio;

    /// <summary><em>Optional</em>. Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side. The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should not exceed 320. Ignored if the file is not uploaded using <see cref="InputFileStream"/>. Thumbnails can't be reused and can be only uploaded as a new file, so you can use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    public InputFile? Thumbnail { get; set; }

    /// <summary><em>Optional</em>. Duration of the audio in seconds</summary>
    public int Duration { get; set; }

    /// <summary><em>Optional</em>. Performer of the audio</summary>
    public string? Performer { get; set; }

    /// <summary><em>Optional</em>. Title of the audio</summary>
    public string? Title { get; set; }

    /// <summary>Initializes an instance of <see cref="InputMediaAudio"/></summary>
    /// <param name="media">File to send. Pass a FileId to send a file that exists on the Telegram servers (recommended), pass an HTTP URL for Telegram to get a file from the Internet, or use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    [SetsRequiredMembers]
    public InputMediaAudio(InputFile media) : base(media) { }

    /// <summary>Instantiates a new <see cref="InputMediaAudio"/></summary>
    public InputMediaAudio() { }
}

/// <summary>Represents a general file to be sent.</summary>
public partial class InputMediaDocument : InputMedia, IInputMediaThumb, IAlbumInputMedia
{
    /// <summary>Type of the result, always <see cref="InputMediaType.Document"/></summary>
    public override InputMediaType Type => InputMediaType.Document;

    /// <summary><em>Optional</em>. Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side. The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should not exceed 320. Ignored if the file is not uploaded using <see cref="InputFileStream"/>. Thumbnails can't be reused and can be only uploaded as a new file, so you can use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    public InputFile? Thumbnail { get; set; }

    /// <summary><em>Optional</em>. Disables automatic server-side content type detection for files uploaded using <see cref="InputFileStream"/>. Always <see langword="true"/>, if the document is sent as part of an album.</summary>
    [JsonPropertyName("disable_content_type_detection")]
    public bool DisableContentTypeDetection { get; set; }

    /// <summary>Initializes an instance of <see cref="InputMediaDocument"/></summary>
    /// <param name="media">File to send. Pass a FileId to send a file that exists on the Telegram servers (recommended), pass an HTTP URL for Telegram to get a file from the Internet, or use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    [SetsRequiredMembers]
    public InputMediaDocument(InputFile media) : base(media) { }

    /// <summary>Instantiates a new <see cref="InputMediaDocument"/></summary>
    public InputMediaDocument() { }
}
