using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
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

        string result = JsonConvert.SerializeObject(container);

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

        Container? result = JsonConvert.DeserializeObject<Container>(jsonData);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_ChatBoostSourceType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        Container? result = JsonConvert.DeserializeObject<Container>(jsonData);

        Assert.NotNull(result);
        Assert.Equal((ChatBoostSourceType)0, result.Type);
    }

    [Fact]
    public void Should_Throw_NotSupportedException_For_Incorrect_ChatBoostSourceType()
    {
        Container container = new() { Type = (ChatBoostSourceType)int.MaxValue };

        Assert.Throws<NotSupportedException>(() => JsonConvert.SerializeObject(container));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    class Container
    {
        [JsonProperty(Required = Required.Always)]
        public ChatBoostSourceType Type { get; init; }
    }
}
