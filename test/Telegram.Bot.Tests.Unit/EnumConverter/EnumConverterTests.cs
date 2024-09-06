using System.ComponentModel;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class EnumConverterTests
{
    public static TheoryData<StickerType, string> TestData => new()
    {
        { StickerType.Regular, "regular" },
        { StickerType.Mask, "mask" },
        { StickerType.CustomEmoji, "custom_emoji" }
    };

    [Theory]
    [MemberData(nameof(TestData))]
    public void Should_Convert_Enum_To_String(StickerType enumValue, string value)
    {
        Container container = new() { Value = enumValue };
        string expectedResult = @$"{{""value"":""{value}""}}";
        string result = JsonSerializer.Serialize(container, JsonBotAPI.Options);
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void Should_Convert_String_To_Enum(StickerType enumValue, string value)
    {
        Container expectedResult = new() { Value = enumValue };
        string jsonData = @$"{{""value"":""{value}""}}";
        Container? result = JsonSerializer.Deserialize<Container>(jsonData, JsonBotAPI.Options);
        Assert.NotNull(result);
        Assert.Equal(expectedResult.Value, result.Value);
    }

    [Fact]
    public void Should_Return_Zero_For_Invalid_Value()
    {
        string jsonData = @$"{{""value"":""invalid value""}}";
        Container? result = JsonSerializer.Deserialize<Container>(jsonData, JsonBotAPI.Options);
        Assert.NotNull(result);
        Assert.Equal((StickerType)0, result.Value);
    }

    [Fact]
    public void Should_Return_Zero_For_Invalid_Numeric_String()
    {
        string jsonData = @$"{{""value"":""{int.MaxValue}""}}";
        Container? result = JsonSerializer.Deserialize<Container>(jsonData, JsonBotAPI.Options);
        Assert.NotNull(result);
        Assert.Equal((StickerType)0, result.Value);
    }

    [Fact]
    public void Should_Throw_For_Numeric_Value()
    {
        string jsonData = @$"{{""value"":1}}";
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Container>(jsonData, JsonBotAPI.Options));
    }

    [Fact]
    public void Should_Throw_For_Invalid_Serialization_Values()
    {
        Container container = new() { Value = (StickerType)int.MaxValue };
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(container, JsonBotAPI.Options));
        container = new() { Value = 0 };
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(container, JsonBotAPI.Options));
    }

    [Fact]
    public void Should_Return_Zero_For_Supported_Unknown()
    {
        string expectedResult = @"""unknown""";
        Assert.Equal((MessageType)0, MessageType.Unknown);
        string result = JsonSerializer.Serialize((MessageType)0, JsonBotAPI.Options);
        Assert.Equal(expectedResult, result);
        var messageType = JsonSerializer.Deserialize<MessageType>(result, JsonBotAPI.Options);
        Assert.Equal((MessageType)0, messageType);
    }


    class Container
    {
        [JsonRequired]
        public StickerType? Value { get; init; }
    }
}
