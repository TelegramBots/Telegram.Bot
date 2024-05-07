namespace Telegram.Bot.Types.Enums;

/// <summary>
/// Format of the <see cref="Sticker"/>
/// </summary>
[JsonConverter(typeof(StickerFormatConverter))]
public enum StickerFormat
{
    /// <summary>
    /// Static <see cref="Sticker"/>
    /// </summary>
    Static = 1,
    /// <summary>
    /// Animated <see cref="Sticker"/>
    /// </summary>
    Animated,
    /// <summary>
    /// Video <see cref="Sticker"/>
    /// </summary>
    Video,
}
