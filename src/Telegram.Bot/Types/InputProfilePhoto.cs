// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes a profile photo to set. Currently, it can be one of<br/><see cref="InputProfilePhotoStatic"/>, <see cref="InputProfilePhotoAnimated"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<InputProfilePhoto>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(InputProfilePhotoStatic), "static")]
[CustomJsonDerivedType(typeof(InputProfilePhotoAnimated), "animated")]
public abstract partial class InputProfilePhoto
{
    /// <summary>Type of the profile photo</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract InputProfilePhotoType Type { get; }
}

/// <summary>A static profile photo in the .JPG format.</summary>
public partial class InputProfilePhotoStatic : InputProfilePhoto
{
    /// <summary>Type of the profile photo, always <see cref="InputProfilePhotoType.Static"/></summary>
    public override InputProfilePhotoType Type => InputProfilePhotoType.Static;

    /// <summary>The static profile photo. Profile photos can't be reused and can only be uploaded as a new file, so you can use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public InputFile Photo { get; set; } = default!;
}

/// <summary>An animated profile photo in the MPEG4 format.</summary>
public partial class InputProfilePhotoAnimated : InputProfilePhoto
{
    /// <summary>Type of the profile photo, always <see cref="InputProfilePhotoType.Animated"/></summary>
    public override InputProfilePhotoType Type => InputProfilePhotoType.Animated;

    /// <summary>The animated profile photo. Profile photos can't be reused and can only be uploaded as a new file, so you can use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public InputFile Animation { get; set; } = default!;

    /// <summary><em>Optional</em>. Timestamp in seconds of the frame that will be used as the static profile photo. Defaults to 0.0.</summary>
    [JsonPropertyName("main_frame_timestamp")]
    public double? MainFrameTimestamp { get; set; }
}
