using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// The background is a wallpaper in the JPEG format
/// </summary>
public class BackgroundTypeWallpaper : BackgroundType
{
    /// <inheritdoc />
    public override BackgroundTypeKind Type => BackgroundTypeKind.Wallpaper;

    /// <summary>
    /// Document with the wallpaper
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required Document Document { get; set; }

    /// <summary>
    /// Dimming of the background in dark themes, as a percentage; 0-100
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int DarkThemeDimming { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the wallpaper is downscaled to fit in a 450x450 square and then
    /// box-blurred with radius 12
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsBlurred { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the background moves slightly when the device is tilted
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsMoving { get; set; }
}
