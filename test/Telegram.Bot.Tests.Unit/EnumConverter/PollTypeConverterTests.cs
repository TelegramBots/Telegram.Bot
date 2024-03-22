using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class PollTypeConverterTests
{
    [Theory]
    [InlineData(PollType.Regular, "regular")]
    [InlineData(PollType.Quiz, "quiz")]
    public void Should_Convert_ChatType_To_String(PollType pollType, string value)
    {
        Poll poll = new() { Type = pollType };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonSerializer.Serialize(poll, JsonSerializerOptionsProvider.Options);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(PollType.Regular, "regular")]
    [InlineData(PollType.Quiz, "quiz")]
    public void Should_Convert_String_To_PollType(PollType pollType, string value)
    {
        Poll expectedResult = new() { Type = pollType };
        string jsonData = @$"{{""type"":""{value}""}}";

        Poll? result = JsonSerializer.Deserialize<Poll>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_PollType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        Poll? result = JsonSerializer.Deserialize<Poll>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal((PollType)0, result.Type);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_PollType()
    {
        Poll poll = new() { Type = (PollType)int.MaxValue };

        // ToDo: add PollType.Unknown ?
        //    protected override string GetStringValue(PollType value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(poll, JsonSerializerOptionsProvider.Options));
    }


    class Poll
    {
        [JsonRequired]
        public PollType Type { get; init; }
    }
}
