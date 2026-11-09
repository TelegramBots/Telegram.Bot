// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>Type of the theme</summary>
[JsonConverter(typeof(EnumConverter<ChatShareThemeType>))]
public enum ChatShareThemeType
{
    /// <summary>The theme with emoji placed on a background.<br/><br/><i>(<see cref="ChatShareTheme"/> can be cast into <see cref="ChatShareThemeEmoji"/>)</i></summary>
    Emoji = 1,
    /// <summary>The theme based on a unique gift owned by the user or the chat.<br/><br/><i>(<see cref="ChatShareTheme"/> can be cast into <see cref="ChatShareThemeUniqueGift"/>)</i></summary>
    UniqueGift,
}
