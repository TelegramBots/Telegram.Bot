using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Serialization;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// This object represents the content of a media message to be sent
/// </summary>
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(InputMediaAnimation), "animation")]
[CustomJsonDerivedType(typeof(InputMediaAudio), "audio")]
[CustomJsonDerivedType(typeof(InputMediaDocument), "document")]
[CustomJsonDerivedType(typeof(InputMediaPhoto), "photo")]
[CustomJsonDerivedType(typeof(InputMediaVideo), "video")]
public abstract class InputMedia
{
    /// <summary>
    /// Type of the media
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract InputMediaType Type { get; }

    /// <summary>
    /// File to send. Pass a file_id to send a file that exists on the Telegram servers (recommended),
    /// pass an HTTP URL for Telegram to get a file from the Internet, or pass "attach://&lt;file_attach_name&gt;"
    /// to upload a new one using multipart/form-data under &lt;file_attach_name%gt; name.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFile Media { get; init; }

    /// <summary>
    /// Optional. Caption of the photo to be sent, 0-1024 characters
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Caption { get; set; }

    /// <summary>
    /// Optional. List of special entities that appear in the caption, which can be specified instead
    /// of <see cref="ParseMode"/>
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary>
    /// Change, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in a caption
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ParseMode? ParseMode { get; set; }

    /// <summary>
    /// Initialize an object
    /// </summary>
    /// <param name="media">File to send</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    protected InputMedia(InputFile media) => Media = media;

    /// <summary>
    /// Initialize an object
    /// </summary>
    protected InputMedia()
    { }
}

/// <summary>
/// Represents a photo to be sent
/// </summary>
public class InputMediaPhoto :
    InputMedia,
    IAlbumInputMedia
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override InputMediaType Type => InputMediaType.Photo;

    /// <summary>
    /// Optional. Pass <see langword="true"/> if the photo needs to be covered with a spoiler animation
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? HasSpoiler { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true"/>, if the caption must be shown above the message media
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ShowCaptionAboveMedia { get; set; }

    /// <summary>
    /// Initializes a new photo media to send with an <see cref="InputFile"/>
    /// </summary>
    /// <param name="media">File to send</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public InputMediaPhoto(InputFile media)
        : base(media)
    { }

    /// <summary>
    /// Initializes a new photo media to send with an <see cref="InputFile"/>
    /// </summary>
    public InputMediaPhoto()
    { }
}

/// <summary>
/// Represents a video to be sent
/// </summary>
public class InputMediaVideo :
    InputMedia,
    IInputMediaThumb,
    IAlbumInputMedia
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override InputMediaType Type => InputMediaType.Video;

    /// <inheritdoc />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InputFile? Thumbnail { get; set; }

    /// <summary>
    /// Optional. Video width
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Width { get; set; }

    /// <summary>
    /// Optional. Video height
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Height { get; set; }

    /// <summary>
    /// Optional. Video duration
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Duration { get; set; }

    /// <summary>
    /// Optional. Pass True, if the uploaded video is suitable for streaming
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? SupportsStreaming { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true"/> if the video needs to be covered with a spoiler animation
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? HasSpoiler { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true"/>, if the caption must be shown above the message media
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ShowCaptionAboveMedia { get; set; }

    /// <summary>
    /// Initializes a new video media to send with an <see cref="InputFile"/>
    /// </summary>
    /// <param name="media">File to send</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public InputMediaVideo(InputFile media)
        : base(media)
    { }

    /// <summary>
    /// Initializes a new video media to send with an <see cref="InputFile"/>
    /// </summary>
    public InputMediaVideo()
    { }
}

/// <summary>
/// Represents an animation file (GIF or H.264/MPEG-4 AVC video without sound) to be sent.
/// </summary>
public class InputMediaAnimation :
    InputMedia,
    IInputMediaThumb
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override InputMediaType Type => InputMediaType.Animation;

    /// <inheritdoc />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InputFile? Thumbnail { get; set; }

    /// <summary>
    /// Optional. Animation width
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Width { get; set; }

    /// <summary>
    /// Optional. Animation height
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Height { get; set; }

    /// <summary>
    /// Optional. Animation duration
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Duration { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true"/>, if the animation needs to be covered with a spoiler animation
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? HasSpoiler { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true"/>, if the caption must be shown above the message media
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ShowCaptionAboveMedia { get; set; }

    /// <summary>
    /// Initializes a new animation media to send with an <see cref="InputFile"/>
    /// </summary>
    /// <param name="media">File to send</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public InputMediaAnimation(InputFile media)
        : base(media)
    { }

    /// <summary>
    /// Initializes a new animation media to send with an <see cref="InputFile"/>
    /// </summary>
    public InputMediaAnimation()
    { }
}

/// <summary>
/// Represents an audio file to be treated as music to be sent.
/// </summary>
public class InputMediaAudio :
    InputMedia,
    IInputMediaThumb,
    IAlbumInputMedia
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override InputMediaType Type => InputMediaType.Audio;

    /// <inheritdoc />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InputFile? Thumbnail { get; set; }

    /// <summary>
    /// Optional. Duration of the audio in seconds
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Duration { get; set; }

    /// <summary>
    /// Optional. Performer of the audio
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Performer { get; set; }

    /// <summary>
    /// Optional. Title of the audio
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }

    /// <summary>
    /// Initializes a new audio media to send with an <see cref="InputFile"/>
    /// </summary>
    /// <param name="media">File to send</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public InputMediaAudio(InputFile media)
        : base(media)
    { }

    /// <summary>
    /// Initializes a new audio media to send with an <see cref="InputFile"/>
    /// </summary>
    public InputMediaAudio()
    { }
}

/// <summary>
/// Represents a general file to be sent
/// </summary>
public class InputMediaDocument :
    InputMedia,
    IInputMediaThumb,
    IAlbumInputMedia
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override InputMediaType Type => InputMediaType.Document;

    /// <inheritdoc />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InputFile? Thumbnail { get; set; }

    /// <summary>
    /// Optional. Disables automatic server-side content type detection for files uploaded using
    /// multipart/form-data. Always true, if the document is sent as part of an album.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? DisableContentTypeDetection { get; set; }

    /// <summary>
    /// Initializes a new document media to send with an <see cref="InputMedia"/>
    /// </summary>
    /// <param name="media">File to send</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public InputMediaDocument(InputFile media)
        : base(media)
    { }

    /// <summary>
    /// Initializes a new document media to send with an <see cref="InputMedia"/>
    /// </summary>
    public InputMediaDocument()
    { }
}
