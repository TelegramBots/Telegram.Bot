using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class PollTypeConverterTests
{
    [Theory]
    [ClassData(typeof(PollTypeData))]
    public void Should_Convert_ChatType_To_String(SendPollRequest sendPollRequest, string value)
    {
        string expectedResult = $$"""{"chat_id":1234,"question":"q","options":["a","b"],"type":"{{value}}"}""";

        string result = JsonSerializer.Serialize(sendPollRequest, TelegramBotClientJsonSerializerContext.Instance.SendPollRequest);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(PollTypeData))]
    public void Should_Convert_String_To_PollType(SendPollRequest expectedResult, string value)
    {
        string jsonData = $$"""{"chat_id":1234,"question":"q","options":["a","b"],"type":"{{value}}"}""";

        SendPollRequest? result = JsonSerializer.Deserialize(jsonData, TelegramBotClientJsonSerializerContext.Instance.SendPollRequest);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_PollType()
    {
        PollType? result = JsonSerializer.Deserialize(int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.PollType);

        Assert.NotNull(result);
        Assert.Equal((PollType)0, result);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_PollType()
    {
        // ToDo: add PollType.Unknown ?
        //    protected override string GetStringValue(PollType value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize((PollType)int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.PollType));
    }

    private class PollTypeData : IEnumerable<object[]>
    {
        private static SendPollRequest NewSendPollRequest(PollType pollType)
        {
            return new SendPollRequest
            {
                ChatId = 1234,
                Question = "q",
                Options = new[] { "a", "b" },
                Type = pollType,
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewSendPollRequest(PollType.Regular), "regular"];
            yield return [NewSendPollRequest(PollType.Quiz), "quiz"];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
