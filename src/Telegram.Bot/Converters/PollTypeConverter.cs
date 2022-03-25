using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class PollTypeConverter : EnumConverter<PollType>
{
    static readonly IReadOnlyDictionary<string, PollType> StringToEnum =
        new Dictionary<string, PollType>
        {
            {"regular", PollType.Regular},
            {"quiz", PollType.Quiz}
        };

    static readonly IReadOnlyDictionary<PollType, string> EnumToString =
        new Dictionary<PollType, string>
        {
            {PollType.Regular, "regular"},
            {PollType.Quiz, "quiz"}
        };

    protected override PollType GetEnumValue(string value) =>
        StringToEnum.TryGetValue(value, out var enumValue)
            ? enumValue
            : 0;

    protected override string GetStringValue(PollType value) =>
        EnumToString.TryGetValue(value, out var stringValue)
            ? stringValue
            : "unknown";
}