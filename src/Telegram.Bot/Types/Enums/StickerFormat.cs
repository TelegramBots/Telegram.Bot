// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary><see cref="Sticker"/>: Format of the added sticker, must be one of <see cref="Static">Static</see> for a <b>.WEBP</b> or <b>.PNG</b> image, <see cref="Animated">Animated</see> for a <b>.TGS</b> animation, <see cref="Video">Video</see> for a <b>.WEBM</b> video</summary>
[JsonConverter(typeof(EnumConverter<StickerFormat>))]
public enum StickerFormat
{
    /// <summary>“static” format</summary>
    Static = 1,
    /// <summary>“animated” format</summary>
    Animated,
    /// <summary>“video” format</summary>
    Video,
}
