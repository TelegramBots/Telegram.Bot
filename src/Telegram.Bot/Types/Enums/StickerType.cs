// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary><see cref="Sticker"/>: Type of the sticker, currently one of <see cref="Regular">Regular</see>, <see cref="Mask">Mask</see>, <see cref="CustomEmoji">CustomEmoji</see>. The type of the sticker is independent from its format, which is determined by the fields <em>IsAnimated</em> and <em>IsVideo</em>.</summary>
[JsonConverter(typeof(EnumConverter<StickerType>))]
public enum StickerType
{
    /// <summary>“regular” type</summary>
    Regular = 1,
    /// <summary>“mask” type</summary>
    Mask,
    /// <summary>“CustomEmoji” type</summary>
    CustomEmoji,
}
