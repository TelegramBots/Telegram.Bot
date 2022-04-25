using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// Represents an animation file (GIF or H.264/MPEG-4 AVC video without sound) to be sent.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class InputMediaAnimation : InputMediaBase,
    IInputMediaThumb
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public override InputMediaType Type => InputMediaType.Animation;

    /// <inheritdoc />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InputMedia? Thumb { get; set; }

    /// <summary>
    /// Optional. Animation width
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Width { get; set; }

    /// <summary>
    /// Optional. Animation height
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Height { get; set; }

    /// <summary>
    /// Optional. Animation duration
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Duration { get; set; }

    /// <summary>
    /// Initializes a new animation media to send with an <see cref="InputMedia"/>
    /// </summary>
    /// <param name="media">File to send</param>
    public InputMediaAnimation(InputMedia media)
        : base(media)
    { }
}