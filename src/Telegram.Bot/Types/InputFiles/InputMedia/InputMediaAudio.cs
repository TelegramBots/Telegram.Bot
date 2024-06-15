using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

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
