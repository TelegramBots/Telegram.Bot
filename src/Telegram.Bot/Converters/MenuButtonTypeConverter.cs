using System;
using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class MenuButtonTypeConverter : EnumConverter<MenuButtonType>
{
    static readonly IReadOnlyDictionary<string, MenuButtonType> StringToEnum =
        new Dictionary<string, MenuButtonType>
        {
            {"default", MenuButtonType.Default},
            {"commands", MenuButtonType.Commands},
            {"web_app", MenuButtonType.WebApp},

        };

    static readonly IReadOnlyDictionary<MenuButtonType, string> EnumToString =
        new Dictionary<MenuButtonType, string>
        {
            { 0, "unknown" },
            {MenuButtonType.Default, "default"},
            {MenuButtonType.Commands, "commands"},
            {MenuButtonType.WebApp, "web_app"},
        };

    protected override MenuButtonType GetEnumValue(string value) =>
        StringToEnum.TryGetValue(value, out var enumValue)
            ? enumValue
            : 0;

    protected override string GetStringValue(MenuButtonType value) =>
        EnumToString.TryGetValue(value, out var stringValue)
            ? stringValue
            : throw new NotSupportedException();
}
