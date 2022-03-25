using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class InputMediaTypeConverter : EnumConverter<InputMediaType>
{
    static readonly IReadOnlyDictionary<string, InputMediaType> StringToEnum =
        new Dictionary<string, InputMediaType>
        {
            {"photo", InputMediaType.Photo},
            {"video", InputMediaType.Video},
            {"animation", InputMediaType.Animation},
            {"audio", InputMediaType.Audio},
            {"document", InputMediaType.Document}
        };

    static readonly IReadOnlyDictionary<InputMediaType, string> EnumToString =
        new Dictionary<InputMediaType, string>
        {
            {InputMediaType.Photo, "photo"},
            {InputMediaType.Video, "video"},
            {InputMediaType.Animation, "animation"},
            {InputMediaType.Audio, "audio"},
            {InputMediaType.Document, "document"}
        };

    protected override InputMediaType GetEnumValue(string value) =>
        StringToEnum.TryGetValue(value, out var enumValue)
            ? enumValue
            : 0;

    protected override string GetStringValue(InputMediaType value) =>
        EnumToString.TryGetValue(value, out var stringValue)
            ? stringValue
            : "unknown";
}