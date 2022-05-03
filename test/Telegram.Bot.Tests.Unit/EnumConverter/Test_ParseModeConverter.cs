using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class Test_ParseModeConverter
{
    [Theory]
    [InlineData(ParseMode.Markdown, "Markdown")]
    [InlineData(ParseMode.Html, "Html")]
    [InlineData(ParseMode.MarkdownV2, "MarkdownV2")]
    public void Sould_Convert_ParseMode_To_String(ParseMode parseMode, string value)
    {
        SendMessageRequest sendMessageRequest = new SendMessageRequest() { ParseMode = parseMode };
        string expectedResult = @$"{{""parse_mode"":""{value}""}}";

        string result = JsonConvert.SerializeObject(sendMessageRequest);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(ParseMode.Markdown, "Markdown")]
    [InlineData(ParseMode.Html, "Html")]
    [InlineData(ParseMode.MarkdownV2, "MarkdownV2")]
    public void Sould_Convert_String_To_ParseMode(ParseMode parseMode, string value)
    {
        SendMessageRequest expectedResult = new SendMessageRequest() { ParseMode = parseMode };
        string jsonData = @$"{{""parse_mode"":""{value}""}}";

        SendMessageRequest result = JsonConvert.DeserializeObject<SendMessageRequest>(jsonData);

        Assert.Equal(expectedResult.ParseMode, result.ParseMode);
    }

    [Fact]
    public void Sould_Return_Zero_For_Incorrect_ParseMode()
    {
        string jsonData = @$"{{""parse_mode"":""{int.MaxValue}""}}";

        SendMessageRequest result = JsonConvert.DeserializeObject<SendMessageRequest>(jsonData);

        Assert.Equal((ParseMode)0, result.ParseMode);
    }

    [Fact]
    public void Sould_Throw_NotSupportedException_For_Incorrect_ParseMode()
    {
        SendMessageRequest sendMessageRequest = new SendMessageRequest() { ParseMode = (ParseMode)0 };

        // ToDo: add ParseMode.Unknown ?
        //    protected override string GetStringValue(ParseMode value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        NotSupportedException ex = Assert.Throws<NotSupportedException>(() =>
            JsonConvert.SerializeObject(sendMessageRequest));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    private class SendMessageRequest
    {
        [JsonProperty(Required = Required.Always)]
        public ParseMode ParseMode { get; init; }
    }
}
