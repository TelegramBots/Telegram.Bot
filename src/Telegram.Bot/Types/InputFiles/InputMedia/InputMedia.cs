using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// This object represents the content of a media message to be sent
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public abstract class InputMedia
{
    /// <summary>
    /// Type of the media
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public abstract InputMediaType Type { get; }

    /// <summary>
    /// File to send. Pass a file_id to send a file that exists on the Telegram servers (recommended),
    /// pass an HTTP URL for Telegram to get a file from the Internet, or pass "attach://&lt;file_attach_name&gt;"
    /// to upload a new one using multipart/form-data under &lt;file_attach_name%gt; name.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public IInputFile Media { get; }

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
    protected InputMedia(IInputFile media) => Media = media;
}
