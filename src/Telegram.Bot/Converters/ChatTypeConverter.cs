using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class ChatTypeConverter : EnumConverter<ChatType>
{
    static readonly IReadOnlyDictionary<string, ChatType> StringToEnum =
        new Dictionary<string, ChatType>
        {
            {"private", ChatType.Private},
            {"group", ChatType.Group},
            {"channel", ChatType.Channel},
            {"supergroup", ChatType.Supergroup},
            {"sender", ChatType.Sender}
        };

    static readonly IReadOnlyDictionary<ChatType, string> EnumToString =
        new Dictionary<ChatType, string>
        {
            {ChatType.Private, "private"},
            {ChatType.Group, "group"},
            {ChatType.Channel, "channel"},
            {ChatType.Supergroup, "supergroup"},
            {ChatType.Sender, "sender"}
        };

    protected override ChatType GetEnumValue(string value) =>
        StringToEnum.TryGetValue(value, out var enumValue)
            ? enumValue
            : 0;

    protected override string GetStringValue(ChatType value) =>
        EnumToString.TryGetValue(value, out var stringValue)
            ? stringValue
            : "unknown";
}