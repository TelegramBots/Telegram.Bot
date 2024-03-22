using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class MaskPositionPointConverterTests
{
    [Theory]
    [InlineData(MaskPositionPoint.Forehead, "forehead")]
    [InlineData(MaskPositionPoint.Eyes, "eyes")]
    [InlineData(MaskPositionPoint.Mouth, "mouth")]
    [InlineData(MaskPositionPoint.Chin, "chin")]
    public void Should_Convert_MaskPositionPoint_To_String(MaskPositionPoint maskPositionPoint, string value)
    {
        MaskPosition maskPosition = new() { Point = maskPositionPoint };
        string expectedResult = @$"{{""point"":""{value}""}}";

        string result = JsonSerializer.Serialize(maskPosition, JsonSerializerOptionsProvider.Options);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(MaskPositionPoint.Forehead, "forehead")]
    [InlineData(MaskPositionPoint.Eyes, "eyes")]
    [InlineData(MaskPositionPoint.Mouth, "mouth")]
    [InlineData(MaskPositionPoint.Chin, "chin")]
    public void Should_Convert_String_To_MaskPositionPoint(MaskPositionPoint maskPositionPoint, string value)
    {
        MaskPosition expectedResult = new() { Point = maskPositionPoint };
        string jsonData = @$"{{""point"":""{value}""}}";

        MaskPosition? result = JsonSerializer.Deserialize<MaskPosition>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Point, result.Point);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_MaskPositionPoint()
    {
        string jsonData = @$"{{""point"":""{int.MaxValue}""}}";

        MaskPosition? result = JsonSerializer.Deserialize<MaskPosition>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal((MaskPositionPoint)0, result.Point);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_MaskPositionPoint()
    {
        MaskPosition maskPosition = new() { Point = (MaskPositionPoint)int.MaxValue };

        // ToDo: add MaskPositionPoint.Unknown ?
        //    protected override string GetStringValue(MaskPositionPoint value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(maskPosition, JsonSerializerOptionsProvider.Options));
    }


    class MaskPosition
    {
        [JsonRequired]
        public MaskPositionPoint Point { get; init; }
    }
}
