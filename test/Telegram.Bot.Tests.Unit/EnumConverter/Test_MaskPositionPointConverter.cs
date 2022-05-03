using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class Test_MaskPositionPointConverter
{
    [Theory]
    [InlineData(MaskPositionPoint.Forehead, "forehead")]
    [InlineData(MaskPositionPoint.Eyes, "eyes")]
    [InlineData(MaskPositionPoint.Mouth, "mouth")]
    [InlineData(MaskPositionPoint.Chin, "chin")]
    public void Sould_Convert_MaskPositionPoint_To_String(MaskPositionPoint maskPositionPoint, string value)
    {
        MaskPosition maskPosition = new MaskPosition() { Point = maskPositionPoint };
        string expectedResult = @$"{{""point"":""{value}""}}";

        string result = JsonConvert.SerializeObject(maskPosition);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(MaskPositionPoint.Forehead, "forehead")]
    [InlineData(MaskPositionPoint.Eyes, "eyes")]
    [InlineData(MaskPositionPoint.Mouth, "mouth")]
    [InlineData(MaskPositionPoint.Chin, "chin")]
    public void Sould_Convert_String_To_MaskPositionPoint(MaskPositionPoint maskPositionPoint, string value)
    {
        MaskPosition expectedResult = new MaskPosition() { Point = maskPositionPoint };
        string jsonData = @$"{{""point"":""{value}""}}";

        MaskPosition result = JsonConvert.DeserializeObject<MaskPosition>(jsonData);

        Assert.Equal(expectedResult.Point, result.Point);
    }

    [Fact]
    public void Sould_Return_Zero_For_Incorrect_MaskPositionPoint()
    {
        string jsonData = @$"{{""point"":""{int.MaxValue}""}}";

        MaskPosition result = JsonConvert.DeserializeObject<MaskPosition>(jsonData);

        Assert.Equal((MaskPositionPoint)0, result.Point);
    }

    [Fact]
    public void Sould_Throw_NotSupportedException_For_Incorrect_MaskPositionPoint()
    {
        MaskPosition maskPosition = new MaskPosition() { Point = (MaskPositionPoint)int.MaxValue };

        // ToDo: add MaskPositionPoint.Unknown ?
        //    protected override string GetStringValue(MaskPositionPoint value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        NotSupportedException ex = Assert.Throws<NotSupportedException>(() =>
            JsonConvert.SerializeObject(maskPosition));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    private class MaskPosition
    {
        [JsonProperty(Required = Required.Always)]
        public MaskPositionPoint Point { get; init; }
    }
}
