namespace Telegram.Bot.Types;

/// <summary>
/// This object represents one size of a photo or a <see cref="Document">file</see> / <see cref="Sticker">sticker</see> thumbnail.
/// </summary>
/// <remarks>A missing thumbnail for a file (or sticker) is presented as an empty object.</remarks>

public class PhotoSize : FileBase
{
    /// <summary>
    /// Photo width
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Width { get; set; }

    /// <summary>
    /// Photo height
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Height { get; set; }
}
