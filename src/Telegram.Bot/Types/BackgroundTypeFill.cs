using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// The background is automatically filled based on the selected colors
/// </summary>
public class BackgroundTypeFill : BackgroundType
{
    /// <inheritdoc />
    public override BackgroundTypeKind Type => BackgroundTypeKind.Fill;

    /// <summary>
    /// Dimming of the background in dark themes, as a percentage; 0-100
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int DarkThemeDimming { get; set; }
}
