using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

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
