using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class ChatBoostSourceTypeConverterTests
{
    [Theory]
    [InlineData(ChatBoostSourceType.Giveaway, "giveaway")]
    [InlineData(ChatBoostSourceType.Premium, "premium")]
    [InlineData(ChatBoostSourceType.GiftCode, "gift_code")]
    public void Should_Convert_ChatBoostSourceType_To_String(ChatBoostSourceType kind, string value)
    {
        Container container = new() { Type = kind };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonSerializer.Serialize(container, JsonBotAPI.Options);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(ChatBoostSourceType.Giveaway, "giveaway")]
    [InlineData(ChatBoostSourceType.Premium, "premium")]
    [InlineData(ChatBoostSourceType.GiftCode, "gift_code")]
    public void Should_Convert_String_ToChatBoostSourceType(ChatBoostSourceType kind, string value)
    {
        Container expectedResult = new() { Type = kind };
        string jsonData = @$"{{""type"":""{value}""}}";

        Container? result = JsonSerializer.Deserialize<Container>(jsonData, JsonBotAPI.Options);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_ChatBoostSourceType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        Container? result = JsonSerializer.Deserialize<Container>(jsonData, JsonBotAPI.Options);

        Assert.NotNull(result);
        Assert.Equal((ChatBoostSourceType)0, result.Type);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_ChatBoostSourceType()
    {
        Container container = new() { Type = (ChatBoostSourceType)int.MaxValue };

        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(container, JsonBotAPI.Options));
    }


    class Container
    {
        [JsonRequired]
        public ChatBoostSourceType Type { get; init; }
    }
}
