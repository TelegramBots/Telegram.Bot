using Newtonsoft.Json;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// Represents a photo to be sent
/// </summary>
public class InputMediaPhoto : InputMediaBase,
    IAlbumInputMedia
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public override InputMediaType Type => InputMediaType.Photo;

    /// <summary>
    /// Initializes a new photo media to send with an <see cref="InputMedia"/>
    /// </summary>
    /// <param name="media">File to send</param>
    public InputMediaPhoto(InputMedia media)
        : base(media)
    { }
}