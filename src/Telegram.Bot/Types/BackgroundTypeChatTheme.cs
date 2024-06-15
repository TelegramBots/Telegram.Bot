using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

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
