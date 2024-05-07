using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class MaskPositionPointConverterTests
{
    [Theory]
    [ClassData(typeof(MaskPositionData))]
    public void Should_Convert_MaskPositionPoint_To_String(MaskPosition maskPosition, string value)
    {
        string expectedResult = $$"""{"point":"{{value}}","x_shift":1,"y_shift":1,"scale":1}""";

        string result = JsonSerializer.Serialize(maskPosition, TelegramBotClientJsonSerializerContext.Instance.MaskPosition);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(MaskPositionData))]
    public void Should_Convert_String_To_MaskPositionPoint(MaskPosition expectedResult, string value)
    {
        string jsonData = $$"""{"point":"{{value}}","x_shift":1,"y_shift":1,"scale":1}""";

        MaskPosition? result = JsonSerializer.Deserialize(jsonData, TelegramBotClientJsonSerializerContext.Instance.MaskPosition);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Point, result.Point);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_MaskPositionPoint()
    {
        MaskPositionPoint? result = JsonSerializer.Deserialize(int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.MaskPositionPoint);

        Assert.NotNull(result);
        Assert.Equal((MaskPositionPoint)0, result);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_MaskPositionPoint()
    {
        // ToDo: add MaskPositionPoint.Unknown ?
        //    protected override string GetStringValue(MaskPositionPoint value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        Assert.Throws<JsonException>(() =>
            JsonSerializer.Serialize((MaskPositionPoint)int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.MaskPositionPoint));
    }

    private class MaskPositionData : IEnumerable<object[]>
    {
        private static MaskPosition NewMaskPosition(MaskPositionPoint maskPositionPoint)
        {
            return new MaskPosition
            {
                Point = maskPositionPoint,
                XShift = 1,
                YShift = 1,
                Scale = 1,
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewMaskPosition(MaskPositionPoint.Forehead), "forehead"];
            yield return [NewMaskPosition(MaskPositionPoint.Eyes), "eyes"];
            yield return [NewMaskPosition(MaskPositionPoint.Mouth), "mouth"];
            yield return [NewMaskPosition(MaskPositionPoint.Chin), "chin"];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
