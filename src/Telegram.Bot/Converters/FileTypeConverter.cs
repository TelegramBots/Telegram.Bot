using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class FileTypeConverter : EnumConverter<FileType>
{
    static readonly IReadOnlyDictionary<string, FileType> StringToEnum =
        new Dictionary<string, FileType>
        {
            {"stream", FileType.Stream},
            {"id", FileType.Id},
            {"url", FileType.Url}
        };

    static readonly IReadOnlyDictionary<FileType, string> EnumToString =
        new Dictionary<FileType, string>
        {
            {FileType.Stream, "stream"},
            {FileType.Id, "id"},
            {FileType.Url, "url"}
        };

    protected override FileType GetEnumValue(string value) =>
        StringToEnum.TryGetValue(value, out var enumValue)
            ? enumValue
            : 0;

    protected override string GetStringValue(FileType value) =>
        EnumToString.TryGetValue(value, out var stringValue)
            ? stringValue
            : "unknown";
}