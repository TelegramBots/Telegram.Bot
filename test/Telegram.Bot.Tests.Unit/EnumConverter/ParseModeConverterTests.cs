using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class ParseModeConverterTests
{
    [Theory]
    [ClassData(typeof(ParseModeData))]
    public void Should_Convert_ParseMode_To_String(SendMessageRequest sendMessageRequest, string value)
    {
        string expectedResult = $$"""{"chat_id":1234,"text":"t","parse_mode":"{{value}}"}""";

        string result = JsonSerializer.Serialize(sendMessageRequest, TelegramBotClientJsonSerializerContext.Instance.SendMessageRequest);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(ParseModeData))]
    public void Should_Convert_String_To_ParseMode(SendMessageRequest expectedResult, string value)
    {
        string jsonData = $$"""{"chat_id":1234,"text":"t","parse_mode":"{{value}}"}""";

        SendMessageRequest? result = JsonSerializer.Deserialize(jsonData, TelegramBotClientJsonSerializerContext.Instance.SendMessageRequest);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.ParseMode, result.ParseMode);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_ParseMode()
    {
        ParseMode? result = JsonSerializer.Deserialize(int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.ParseMode);

        Assert.NotNull(result);
        Assert.Equal((ParseMode)0, result);
    }

    private class ParseModeData : IEnumerable<object[]>
    {
        private static SendMessageRequest NewSendMessageRequest(ParseMode parseMode)
        {
            return new SendMessageRequest
            {
                ParseMode = parseMode,
                ChatId = 1234,
                Text = "t"
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewSendMessageRequest(ParseMode.Markdown), "Markdown"];
            yield return [NewSendMessageRequest(ParseMode.Html), "Html"];
            yield return [NewSendMessageRequest(ParseMode.MarkdownV2), "MarkdownV2"];
            yield return [NewSendMessageRequest((ParseMode)0), "unknown"];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
