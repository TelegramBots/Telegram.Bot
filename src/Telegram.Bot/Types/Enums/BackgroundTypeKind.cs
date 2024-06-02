namespace Telegram.Bot.Types.Enums;

/// <summary>
/// Represents different types of background kinds.
/// </summary>
[JsonConverter(typeof(BackgroundTypeKindConverter))]
public enum BackgroundTypeKind
{
    /// <summary>
    /// A solid fill background.
    /// </summary>
    Fill = 1,

    /// <summary>
    /// A wallpaper background.
    /// </summary>
    Wallpaper,

    /// <summary>
    /// A patterned background.
    /// </summary>
    Pattern,

    /// <summary>
    /// A chat theme background.
    /// </summary>
    ChatTheme,
}
