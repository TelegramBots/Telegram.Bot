using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class ReactionTypeKindConverterTests
{
    [Theory]
    [InlineData(ReactionTypeKind.Emoji, "emoji")]
    [InlineData(ReactionTypeKind.CustomEmoji, "custom_emoji")]
    public void Should_Convert_ChatAction_To_String(ReactionTypeKind kind, string value)
    {
        Container container = new() { Type = kind };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonSerializer.Serialize(container, JsonSerializerOptionsProvider.Options);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(ReactionTypeKind.Emoji, "emoji")]
    [InlineData(ReactionTypeKind.CustomEmoji, "custom_emoji")]
    public void Should_Convert_String_ToChatAction(ReactionTypeKind kind, string value)
    {
        Container expectedResult = new() { Type = kind };
        string jsonData = @$"{{""type"":""{value}""}}";

        Container? result = JsonSerializer.Deserialize<Container>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_ChatAction()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        Container? result = JsonSerializer.Deserialize<Container>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal((ReactionTypeKind)0, result.Type);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_ChatAction()
    {
        Container container = new() { Type = (ReactionTypeKind)int.MaxValue };

        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(container, JsonSerializerOptionsProvider.Options));
    }


    class Container
    {
        [JsonRequired]
        public ReactionTypeKind Type { get; init; }
    }
}
