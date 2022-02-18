using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class ParseModeConverter : EnumConverter<ParseMode>
{
    static readonly IReadOnlyDictionary<string, ParseMode> StringToEnum =
        new Dictionary<string, ParseMode>
        {
            {"Markdown", ParseMode.Markdown},
            {"Html", ParseMode.Html},
            {"MarkdownV2", ParseMode.MarkdownV2}
        };

    static readonly IReadOnlyDictionary<ParseMode, string> EnumToString =
        new Dictionary<ParseMode, string>
        {
            {ParseMode.Markdown, "Markdown"},
            {ParseMode.Html, "Html"},
            {ParseMode.MarkdownV2, "MarkdownV2"}
        };

    protected override ParseMode GetEnumValue(string value) =>
        StringToEnum.TryGetValue(value, out var enumValue)
            ? enumValue
            : 0;

    protected override string GetStringValue(ParseMode value) =>
        EnumToString.TryGetValue(value, out var stringValue)
            ? stringValue
            : "unknown";
}