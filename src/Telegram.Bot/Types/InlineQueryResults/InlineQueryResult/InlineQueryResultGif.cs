using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents a link to an animated GIF file. By default, this animated GIF file will be sent by the
/// user with optional caption. Alternatively, you can use
/// <see cref="InlineQueryResultGif.InputMessageContent"/> to send a message with the
/// specified content instead of the animation.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class InlineQueryResultGif : InlineQueryResult
{
    /// <summary>
    /// Type of the result, must be gif
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public override InlineQueryResultType Type => InlineQueryResultType.Gif;

    /// <summary>
    /// A valid URL for the GIF file. File size must not exceed 1MB
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string GifUrl { get; }

    /// <summary>
    /// Optional. Width of the GIF.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? GifWidth { get; set; }

    /// <summary>
    /// Optional. Height of the GIF.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? GifHeight { get; set; }

    /// <summary>
    /// Optional. Duration of the GIF.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? GifDuration { get; set; }

    /// <summary>
    /// URL of the static (JPEG or GIF) or animated (MPEG4) thumbnail for the result
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string ThumbUrl { get; }

    /// <summary>
    /// Optional. MIME type of the thumbnail, must be one of “image/jpeg”, “image/gif”,
    /// or “video/mp4”. Defaults to “image/jpeg”
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? ThumbMimeType { get; set; }

    /// <summary>
    /// Optional. Title for the result
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Title { get; set; }

    /// <inheritdoc cref="Documentation.Caption" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Caption { get; set; }

    /// <inheritdoc cref="Documentation.ParseMode" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ParseMode? ParseMode { get; set; }

    /// <inheritdoc cref="Documentation.CaptionEntities" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <inheritdoc cref="Documentation.InputMessageContent" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    /// <param name="id">Unique identifier of this result</param>
    /// <param name="gifUrl">Width of the GIF</param>
    /// <param name="thumbUrl">Url of the thumbnail for the result.</param>
    public InlineQueryResultGif(string id, string gifUrl, string thumbUrl)
        : base(id)
    {
        GifUrl = gifUrl;
        ThumbUrl = thumbUrl;
    }
}
