using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class ParseModeConverterTests
{
    [Theory]
    [InlineData(ParseMode.Markdown, "Markdown")]
    [InlineData(ParseMode.Html, "Html")]
    [InlineData(ParseMode.MarkdownV2, "MarkdownV2")]
    [InlineData((ParseMode)0, "unknown")]
    public void Should_Convert_ParseMode_To_String(ParseMode parseMode, string value)
    {
        SendMessageRequest sendMessageRequest = new() { ParseMode = parseMode };
        string expectedResult = @$"{{""parse_mode"":""{value}""}}";

        string result = JsonConvert.SerializeObject(sendMessageRequest);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(ParseMode.Markdown, "Markdown")]
    [InlineData(ParseMode.Html, "Html")]
    [InlineData(ParseMode.MarkdownV2, "MarkdownV2")]
    [InlineData((ParseMode)0, "unknown")]
    public void Should_Convert_String_To_ParseMode(ParseMode parseMode, string value)
    {
        SendMessageRequest expectedResult = new() { ParseMode = parseMode };
        string jsonData = @$"{{""parse_mode"":""{value}""}}";

        SendMessageRequest? result = JsonConvert.DeserializeObject<SendMessageRequest>(jsonData);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.ParseMode, result.ParseMode);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_ParseMode()
    {
        string jsonData = @$"{{""parse_mode"":""{int.MaxValue}""}}";

        SendMessageRequest? result = JsonConvert.DeserializeObject<SendMessageRequest>(jsonData);

        Assert.NotNull(result);
        Assert.Equal((ParseMode)0, result.ParseMode);
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    class SendMessageRequest
    {
        [JsonProperty(Required = Required.Always)]
        public ParseMode ParseMode { get; init; }
    }
}
