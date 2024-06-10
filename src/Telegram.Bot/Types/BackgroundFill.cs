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
    public abstract BackgroundFillKind Type { get; }
}
