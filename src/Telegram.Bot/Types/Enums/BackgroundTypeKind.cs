// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>Type of the background</summary>
[JsonConverter(typeof(EnumConverter<BackgroundTypeKind>))]
public enum BackgroundTypeKind
{
    /// <summary>The background is automatically filled based on the selected colors.<br/><br/><i>(<see cref="BackgroundType"/> can be cast into <see cref="BackgroundTypeFill"/>)</i></summary>
    Fill = 1,
    /// <summary>The background is a wallpaper in the JPEG format.<br/><br/><i>(<see cref="BackgroundType"/> can be cast into <see cref="BackgroundTypeWallpaper"/>)</i></summary>
    Wallpaper,
    /// <summary>The background is a .PNG or .TGV (gzipped subset of SVG with MIME type “application/x-tgwallpattern”) pattern to be combined with the background fill chosen by the user.<br/><br/><i>(<see cref="BackgroundType"/> can be cast into <see cref="BackgroundTypePattern"/>)</i></summary>
    Pattern,
    /// <summary>The background is taken directly from a built-in chat theme.<br/><br/><i>(<see cref="BackgroundType"/> can be cast into <see cref="BackgroundTypeChatTheme"/>)</i></summary>
    ChatTheme,
}
