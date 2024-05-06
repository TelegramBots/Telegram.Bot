using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class InputMediaTypeConverterTests
{
    [Theory]
    [ClassData(typeof(InputMediaData))]
    public void Should_Convert_InputMediaType_To_String(InputMedia inputMedia, string value)
    {
        string expectedResult = $$"""{"type":"{{value}}","media":"1"}""";

        string result = JsonSerializer.Serialize(inputMedia, TelegramBotClientJsonSerializerContext.Instance.InputMedia);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(InputMediaData))]
    public void Should_Convert_String_To_InputMediaType(InputMedia expectedResult, string value)
    {
        string jsonData = $$"""{"type":"{{value}}","media":"1"}""";

        InputMedia? result = JsonSerializer.Deserialize(jsonData, TelegramBotClientJsonSerializerContext.Instance.InputMedia);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_InputMediaType()
    {
        InputMediaType? result = JsonSerializer.Deserialize(int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.InputMediaType);

        Assert.NotNull(result);
        Assert.Equal((InputMediaType)0, result);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_InputMediaType()
    {
        // ToDo: add InputMediaType.Unknown ?
        //    protected override string GetStringValue(InputMediaType value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        Assert.Throws<JsonException>(() =>
            JsonSerializer.Serialize((InputMediaType)int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.InputMediaType));
    }

    private class InputMediaData : IEnumerable<object[]>
    {
        private static InputMedia NewInputMedia(InputMediaType inputMediaType)
        {
            return inputMediaType switch
            {
                InputMediaType.Photo => new InputMediaPhoto { Media = new InputFileId {Id = "1"} },
                InputMediaType.Video => new InputMediaVideo { Media = new InputFileId {Id = "1"} },
                InputMediaType.Animation => new InputMediaAnimation { Media = new InputFileId {Id = "1"} },
                InputMediaType.Audio => new InputMediaAudio { Media = new InputFileId {Id = "1"} },
                InputMediaType.Document => new InputMediaDocument { Media = new InputFileId {Id = "1"} },
                _ => throw new ArgumentOutOfRangeException(nameof(inputMediaType), inputMediaType, null),
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewInputMedia(InputMediaType.Photo), "photo"];
            yield return [NewInputMedia(InputMediaType.Video), "video"];
            yield return [NewInputMedia(InputMediaType.Animation), "animation"];
            yield return [NewInputMedia(InputMediaType.Audio), "audio"];
            yield return [NewInputMedia(InputMediaType.Document), "document"];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
