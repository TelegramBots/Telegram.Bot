using Telegram.Bot.Serialization;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// This object describes the way a background is filled based on the selected colors. Currently, it can be one of
/// <list type="bullet">
/// <item><see cref="BackgroundFillSolid"/></item>
/// <item><see cref="BackgroundFillGradient"/></item>
/// <item><see cref="BackgroundFillFreeformGradient"/></item>
/// </list>
/// </summary>
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(BackgroundFillSolid), "solid")]
[CustomJsonDerivedType(typeof(BackgroundFillGradient), "gradient")]
[CustomJsonDerivedType(typeof(BackgroundFillFreeformGradient), "freeform_gradient")]
public abstract class BackgroundFill
{
    /// <summary>
    /// Type of the background fill
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract BackgroundFillType Type { get; }
}

/// <summary>
/// The background is filled using the selected color
/// </summary>
public class BackgroundFillSolid : BackgroundFill
{
    /// <inheritdoc />
    public override BackgroundFillType Type => BackgroundFillType.Solid;

    /// <summary>
    /// The color of the background fill in the RGB24 format
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int Color { get; set; }
}

/// <summary>
/// The background is a gradient fill
/// </summary>
public class BackgroundFillGradient : BackgroundFill
{
    /// <inheritdoc />
    public override BackgroundFillType Type => BackgroundFillType.Gradient;

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

/// <summary>
/// The background is a freeform gradient that rotates after every message in the chat
/// </summary>
public class BackgroundFillFreeformGradient : BackgroundFill
{
    /// <inheritdoc />
    public override BackgroundFillType Type => BackgroundFillType.FreeformGradient;

    /// <summary>
    /// A list of the 3 or 4 base colors that are used to generate the freeform gradient in the RGB24 format
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int[] Colors { get; set; }
}
