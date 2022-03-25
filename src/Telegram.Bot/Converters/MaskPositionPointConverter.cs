using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class MaskPositionPointConverter : EnumConverter<MaskPositionPoint>
{
    static readonly IReadOnlyDictionary<string, MaskPositionPoint> StringToEnum =
        new Dictionary<string, MaskPositionPoint>
        {
            {"forehead", MaskPositionPoint.Forehead},
            {"eyes", MaskPositionPoint.Eyes},
            {"mouth", MaskPositionPoint.Mouth},
            {"chin", MaskPositionPoint.Chin}
        };

    static readonly IReadOnlyDictionary<MaskPositionPoint, string> EnumToString =
        new Dictionary<MaskPositionPoint, string>
        {
            {MaskPositionPoint.Forehead, "forehead"},
            {MaskPositionPoint.Eyes, "eyes"},
            {MaskPositionPoint.Mouth, "mouth"},
            {MaskPositionPoint.Chin, "chin"}
        };

    protected override MaskPositionPoint GetEnumValue(string value) =>
        StringToEnum.TryGetValue(value, out var enumValue)
            ? enumValue
            : 0;

    protected override string GetStringValue(MaskPositionPoint value) =>
        EnumToString.TryGetValue(value, out var stringValue)
            ? stringValue
            : "unknown";
}