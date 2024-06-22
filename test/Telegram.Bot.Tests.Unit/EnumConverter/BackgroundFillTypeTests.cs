using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class BackgroundFillTypeTests : BaseEnumMemberConverterTests<BackgroundFillType>
{
    public static IEnumerable<object[]> TestData => [
        [BackgroundFillType.Solid, "solid"],
        [BackgroundFillType.Gradient, "gradient"],
        [BackgroundFillType.FreeformGradient, "freeform_gradient"],
    ];
}
