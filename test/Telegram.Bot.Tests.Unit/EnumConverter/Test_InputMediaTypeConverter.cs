using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class Test_InputMediaTypeConverter
{
    [Theory]
    [InlineData(InputMediaType.Photo, "photo")]
    [InlineData(InputMediaType.Video, "video")]
    [InlineData(InputMediaType.Animation, "animation")]
    [InlineData(InputMediaType.Audio, "audio")]
    [InlineData(InputMediaType.Document, "document")]
    public void Sould_Convert_InputMediaType_To_String(InputMediaType inputMediaType, string value)
    {
        InputMedia inputMedia = new InputMedia() { Type = inputMediaType };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonConvert.SerializeObject(inputMedia);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(InputMediaType.Photo, "photo")]
    [InlineData(InputMediaType.Video, "video")]
    [InlineData(InputMediaType.Animation, "animation")]
    [InlineData(InputMediaType.Audio, "audio")]
    [InlineData(InputMediaType.Document, "document")]
    public void Sould_Convert_String_To_InputMediaType(InputMediaType inputMediaType, string value)
    {
        InputMedia expectedResult = new InputMedia() { Type = inputMediaType };
        string jsonData = @$"{{""type"":""{value}""}}";

        InputMedia result = JsonConvert.DeserializeObject<InputMedia>(jsonData);

        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Sould_Return_Zero_For_Incorrect_InputMediaType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        InputMedia result = JsonConvert.DeserializeObject<InputMedia>(jsonData);

        Assert.Equal((InputMediaType)0, result.Type);
    }

    [Fact]
    public void Sould_Throw_NotSupportedException_For_Incorrect_InputMediaType()
    {
        InputMedia inputMedia = new InputMedia() { Type = (InputMediaType)int.MaxValue };

        // ToDo: add InputMediaType.Unknown ?
        //    protected override string GetStringValue(InputMediaType value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        NotSupportedException ex = Assert.Throws<NotSupportedException>(() =>
            JsonConvert.SerializeObject(inputMedia));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    private class InputMedia
    {
        [JsonProperty(Required = Required.Always)]
        public InputMediaType Type { get; init; }
    }
}
