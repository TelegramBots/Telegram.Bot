using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class ChatTypeConverterTests
{
    [Theory]
    [InlineData(ChatType.Private, "private")]
    [InlineData(ChatType.Group, "group")]
    [InlineData(ChatType.Channel, "channel")]
    [InlineData(ChatType.Supergroup, "supergroup")]
    [InlineData(ChatType.Sender, "sender")]
    public void Should_Convert_ChatType_To_String(ChatType chatType, string value)
    {
        InlineQuery inlineQuery = new() { Type = chatType };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonSerializer.Serialize(inlineQuery, JsonSerializerOptionsProvider.Options);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(ChatType.Private, "private")]
    [InlineData(ChatType.Group, "group")]
    [InlineData(ChatType.Channel, "channel")]
    [InlineData(ChatType.Supergroup, "supergroup")]
    [InlineData(ChatType.Sender, "sender")]
    public void Should_Convert_String_To_ChatType(ChatType chatType, string value)
    {
        InlineQuery expectedResult = new() { Type = chatType };
        string jsonData = @$"{{""type"":""{value}""}}";

        InlineQuery? result = JsonSerializer.Deserialize<InlineQuery>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_ChatType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        InlineQuery? result = JsonSerializer.Deserialize<InlineQuery>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal((ChatType)0, result.Type);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_ChatType()
    {
        InlineQuery inlineQuery = new() { Type = (ChatType)int.MaxValue };

        // ToDo: add ChatType.Unknown ?
        //    protected override string GetStringValue(ChatType value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(inlineQuery, JsonSerializerOptionsProvider.Options));
    }


    class InlineQuery
    {
        [JsonRequired]
        public ChatType Type { get; init; }
    }
}
