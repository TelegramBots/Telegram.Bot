// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes the content of a story to post. Currently, it can be one of<br/><see cref="InputStoryContentPhoto"/>, <see cref="InputStoryContentVideo"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<InputStoryContent>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(InputStoryContentPhoto), "photo")]
[CustomJsonDerivedType(typeof(InputStoryContentVideo), "video")]
public abstract partial class InputStoryContent
{
    /// <summary>Type of the content</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract InputStoryContentType Type { get; }
}

/// <summary>Describes a photo to post as a story.</summary>
public partial class InputStoryContentPhoto : InputStoryContent
{
    /// <summary>Type of the content, always <see cref="InputStoryContentType.Photo"/></summary>
    public override InputStoryContentType Type => InputStoryContentType.Photo;

    /// <summary>The photo to post as a story. The photo must be of the size 1080x1920 and must not exceed 10 MB. The photo can't be reused and can only be uploaded as a new file, so you can use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public InputFile Photo { get; set; } = default!;
}

/// <summary>Describes a video to post as a story.</summary>
public partial class InputStoryContentVideo : InputStoryContent
{
    /// <summary>Type of the content, always <see cref="InputStoryContentType.Video"/></summary>
    public override InputStoryContentType Type => InputStoryContentType.Video;

    /// <summary>The video to post as a story. The video must be of the size 720x1280, streamable, encoded with H.265 codec, with key frames added each second in the MPEG4 format, and must not exceed 30 MB. The video can't be reused and can only be uploaded as a new file, so you can use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public InputFile Video { get; set; } = default!;

    /// <summary><em>Optional</em>. Precise duration of the video in seconds; 0-60</summary>
    public double Duration { get; set; }

    /// <summary><em>Optional</em>. Timestamp in seconds of the frame that will be used as the static cover for the story. Defaults to 0.0.</summary>
    [JsonPropertyName("cover_frame_timestamp")]
    public double? CoverFrameTimestamp { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the video has no sound</summary>
    [JsonPropertyName("is_animation")]
    public bool IsAnimation { get; set; }
}
