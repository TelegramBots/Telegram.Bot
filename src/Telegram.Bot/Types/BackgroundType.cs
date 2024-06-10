using Telegram.Bot.Serialization;
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
