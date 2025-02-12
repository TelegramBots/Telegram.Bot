// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>Type of the background fill</summary>
[JsonConverter(typeof(EnumConverter<BackgroundFillType>))]
public enum BackgroundFillType
{
    /// <summary>The background is filled using the selected color.<br/><br/><i>(<see cref="BackgroundFill"/> can be cast into <see cref="BackgroundFillSolid"/>)</i></summary>
    Solid = 1,
    /// <summary>The background is a gradient fill.<br/><br/><i>(<see cref="BackgroundFill"/> can be cast into <see cref="BackgroundFillGradient"/>)</i></summary>
    Gradient,
    /// <summary>The background is a freeform gradient that rotates after every message in the chat.<br/><br/><i>(<see cref="BackgroundFill"/> can be cast into <see cref="BackgroundFillFreeformGradient"/>)</i></summary>
    FreeformGradient,
}
