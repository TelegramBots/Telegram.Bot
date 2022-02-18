using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// This object represents the content of a media message to be sent
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public abstract class InputMediaBase : IInputMedia
{
    /// <summary>
    /// Type of the media
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public abstract InputMediaType Type { get; }

    /// <summary>
    /// File to send
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public InputMedia Media { get; }

    /// <summary>
    /// Optional. Caption of the photo to be sent, 0-1024 characters
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Caption { get; set; }

    /// <summary>
    /// Optional. List of special entities that appear in the caption, which can be specified instead
    /// of <see cref="ParseMode"/>
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary>
    /// Change, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in a caption
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ParseMode? ParseMode { get; set; }

    /// <summary>
    /// Initialize an object
    /// </summary>
    /// <param name="media">File to send</param>
    protected InputMediaBase(InputMedia media) => Media = media;
}