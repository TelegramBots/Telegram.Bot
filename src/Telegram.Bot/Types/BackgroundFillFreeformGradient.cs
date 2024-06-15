using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// The background is a freeform gradient that rotates after every message in the chat
/// </summary>
public class BackgroundFillFreeformGradient : BackgroundFill
{
    /// <inheritdoc />
    public override BackgroundFillKind Type => BackgroundFillKind.FreeformGradient;

    /// <summary>
    /// A list of the 3 or 4 base colors that are used to generate the freeform gradient in the RGB24 format
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int[] Colors { get; set; }
}
