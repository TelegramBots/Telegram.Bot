using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// Represents a general file to be sent
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class InputMediaDocument :
    InputMedia,
    IInputMediaThumb,
    IAlbumInputMedia
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public override InputMediaType Type => InputMediaType.Document;

    /// <inheritdoc />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public IInputFile? Thumb { get; set; }

    /// <summary>
    /// Optional. Disables automatic server-side content type detection for files uploaded using
    /// multipart/form-data. Always true, if the document is sent as part of an album.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? DisableContentTypeDetection { get; set; }

    /// <summary>
    /// Initializes a new document media to send with an <see cref="InputMedia"/>
    /// </summary>
    /// <param name="media">File to send</param>
    public InputMediaDocument(IInputFile media)
        : base(media)
    { }
}
