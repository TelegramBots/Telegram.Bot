using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// The background is a gradient fill
/// </summary>
public class BackgroundFillGradient : BackgroundFill
{
    /// <inheritdoc />
    public override BackgroundFillKind Type => BackgroundFillKind.Gradient;

    /// <summary>
    /// Top color of the gradient in the RGB24 format
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int TopColor { get; set; }

    /// <summary>
    /// Bottom color of the gradient in the RGB24 format
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int BottomColor { get; set; }

    /// <summary>
    /// Clockwise rotation angle of the background fill in degrees; 0-359
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int RotationAngle { get; set; }
}
