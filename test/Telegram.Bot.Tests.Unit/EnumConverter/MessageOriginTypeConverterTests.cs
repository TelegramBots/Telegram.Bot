using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class MessageOriginTypeConverterTests
{
    [Theory]
    [InlineData(MessageOriginType.User, "user")]
    [InlineData(MessageOriginType.HiddenUser, "hidden_user")]
    [InlineData(MessageOriginType.Chat, "chat")]
    [InlineData(MessageOriginType.Channel, "channel")]
    public void Should_Convert_MessageOriginType_To_String(MessageOriginType messageOriginType, string value)
    {
        MessageOrigin messageOrigin = new() { Type = messageOriginType };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonConvert.SerializeObject(messageOrigin);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(MessageOriginType.User, "user")]
    [InlineData(MessageOriginType.HiddenUser, "hidden_user")]
    [InlineData(MessageOriginType.Chat, "chat")]
    [InlineData(MessageOriginType.Channel, "channel")]
    public void Should_Convert_String_To_MessageOriginType(MessageOriginType messageOriginType, string value)
    {
        MessageOrigin expectedResult = new() { Type = messageOriginType };
        string jsonData = @$"{{""type"":""{value}""}}";

        MessageOrigin? result = JsonConvert.DeserializeObject<MessageOrigin>(jsonData);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_ChatMemberStatus()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        MessageOrigin? result = JsonConvert.DeserializeObject<MessageOrigin>(jsonData);

        Assert.NotNull(result);
        Assert.Equal((MessageOriginType)0, result.Type);
    }

    [Fact]
    public void Should_Throw_NotSupportedException_For_Incorrect_ChatMemberStatus()
    {
        MessageOrigin messageOrigin = new() { Type = (MessageOriginType)int.MaxValue };

        Assert.Throws<NotSupportedException>(() => JsonConvert.SerializeObject(messageOrigin));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    class MessageOrigin
    {
        [JsonProperty(Required = Required.Always)]
        public MessageOriginType Type { get; init; }
    }
}
