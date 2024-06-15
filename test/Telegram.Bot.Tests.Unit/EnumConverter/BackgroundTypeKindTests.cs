using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class BackgroundTypeKindTests : BaseEnumMemberConverterTests<BackgroundTypeKind>
{
    public static IEnumerable<object[]> TestData => [
        [BackgroundTypeKind.Fill, "fill"],
        [BackgroundTypeKind.Wallpaper, "wallpaper"],
        [BackgroundTypeKind.Pattern, "pattern"],
        [BackgroundTypeKind.ChatTheme, "chat_theme"],
    ];
}
