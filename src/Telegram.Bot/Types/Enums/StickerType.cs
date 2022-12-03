namespace Telegram.Bot.Types.Enums;

/// <summary>
/// Type of the <see cref="Sticker"/>
/// </summary>
[JsonConverter(typeof(StickerTypeConverter))]
public enum StickerType
{
    /// <summary>
    /// Regular  <see cref="Sticker"/>
    /// </summary>
    Regular = 1,

    /// <summary>
    /// Mask
    /// </summary>
    Mask,

    /// <summary>
    /// Custom emoji
    /// </summary>
    CustomEmoji
}
