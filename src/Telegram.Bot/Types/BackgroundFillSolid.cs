using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// The background is filled using the selected color
/// </summary>
public class BackgroundFillSolid : BackgroundFill
{
    /// <inheritdoc />
    public override BackgroundFillKind Type => BackgroundFillKind.Solid;

    /// <summary>
    /// The color of the background fill in the RGB24 format
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int Color { get; set; }
}
