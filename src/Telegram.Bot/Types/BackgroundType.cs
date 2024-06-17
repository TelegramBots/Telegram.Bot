using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// This object describes the type of a background. Currently, it can be one of
/// <list type="bullet">
/// <item><see cref="BackgroundTypeFill" /></item>
/// <item><see cref="BackgroundTypeWallpaper" /></item>
/// <item><see cref="BackgroundTypePattern" /></item>
/// <item><see cref="BackgroundTypeChatTheme" /></item>
/// </list>
/// </summary>
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(BackgroundTypeFill), "fill")]
[CustomJsonDerivedType(typeof(BackgroundTypeWallpaper), "wallpaper")]
[CustomJsonDerivedType(typeof(BackgroundTypePattern), "pattern")]
[CustomJsonDerivedType(typeof(BackgroundTypeChatTheme), "chat_theme")]
public abstract class BackgroundType
{
    /// <summary>
    /// Type of the background
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract BackgroundTypeKind Type { get; }
}

/// <summary>
/// The background is automatically filled based on the selected colors
/// </summary>
public class BackgroundTypeFill : BackgroundType
{
    /// <inheritdoc />
    public override BackgroundTypeKind Type => BackgroundTypeKind.Fill;

    /// <summary>
    /// Dimming of the background in dark themes, as a percentage; 0-100
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int DarkThemeDimming { get; set; }
}

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

/// <summary>
/// The background is a PNG or TGV (gzipped subset of SVG with MIME type “application/x-tgwallpattern”) pattern
/// to be combined with the background fill chosen by the user.
/// </summary>
public class BackgroundTypePattern : BackgroundType
{
    /// <inheritdoc />
    public override BackgroundTypeKind Type => BackgroundTypeKind.Pattern;

    /// <summary>
    /// Document with the pattern
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required Document Document { get; set; }

    /// <summary>
    /// The background fill that is combined with the pattern
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required BackgroundFill Fill { get; set; }

    /// <summary>
    /// Intensity of the pattern when it is shown above the filled background; 0-100
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int Intensity { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the background fill must be applied only to the pattern itself.
    /// All other pixels are black in this case. For dark themes only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsInverted { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the background moves slightly when the device is tilted
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsMoving { get; set; }
}

/// <summary>
/// The background is taken directly from a built-in chat theme
/// </summary>
public class BackgroundTypeChatTheme : BackgroundType
{
    /// <inheritdoc />
    public override BackgroundTypeKind Type => BackgroundTypeKind.ChatTheme;

    /// <summary>
    /// Name of the chat theme, which is usually an emoji
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string ThemeName { get; set; }
}
