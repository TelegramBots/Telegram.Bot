using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class BackgroundFillKindTests : BaseEnumMemberConverterTests<BackgroundFillKind>
{
    public static IEnumerable<object[]> TestData => [
        [BackgroundFillKind.Solid, "solid"],
        [BackgroundFillKind.Gradient, "gradient"],
        [BackgroundFillKind.FreeformGradient, "freeform_gradient"],
    ];
}
