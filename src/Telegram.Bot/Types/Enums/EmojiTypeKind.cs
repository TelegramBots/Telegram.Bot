// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>Type of the emoji</summary>
[JsonConverter(typeof(EnumConverter<EmojiTypeKind>))]
public enum EmojiTypeKind
{
    /// <summary>A regular emoji.<br/><br/><i>(<see cref="EmojiType"/> can be cast into <see cref="EmojiTypeEmoji"/>)</i></summary>
    Emoji = 1,
    /// <summary>A custom emoji.<br/><br/><i>(<see cref="EmojiType"/> can be cast into <see cref="EmojiTypeCustomEmoji"/>)</i></summary>
    CustomEmoji,
}
