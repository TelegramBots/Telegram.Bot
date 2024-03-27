using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

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

        string result = JsonSerializer.Serialize(messageOrigin, JsonSerializerOptionsProvider.Options);

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

        MessageOrigin? result = JsonSerializer.Deserialize<MessageOrigin>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_ChatMemberStatus()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        MessageOrigin? result = JsonSerializer.Deserialize<MessageOrigin>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal((MessageOriginType)0, result.Type);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_ChatMemberStatus()
    {
        MessageOrigin messageOrigin = new() { Type = (MessageOriginType)int.MaxValue };

        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(messageOrigin, JsonSerializerOptionsProvider.Options));
    }


    class MessageOrigin
    {
        [JsonRequired]
        public MessageOriginType Type { get; init; }
    }
}
