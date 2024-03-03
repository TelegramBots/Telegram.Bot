using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Telegram.Bot.Types.Enums;
using Xunit;

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

        string result = JsonConvert.SerializeObject(container);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(ReactionTypeKind.Emoji, "emoji")]
    [InlineData(ReactionTypeKind.CustomEmoji, "custom_emoji")]
    public void Should_Convert_String_ToChatAction(ReactionTypeKind kind, string value)
    {
        Container expectedResult = new() { Type = kind };
        string jsonData = @$"{{""type"":""{value}""}}";

        Container? result = JsonConvert.DeserializeObject<Container>(jsonData);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_ChatAction()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        Container? result = JsonConvert.DeserializeObject<Container>(jsonData);

        Assert.NotNull(result);
        Assert.Equal((ReactionTypeKind)0, result.Type);
    }

    [Fact]
    public void Should_Throw_NotSupportedException_For_Incorrect_ChatAction()
    {
        Container container = new() { Type = (ReactionTypeKind)int.MaxValue };

        Assert.Throws<NotSupportedException>(() => JsonConvert.SerializeObject(container));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    class Container
    {
        [JsonProperty(Required = Required.Always)]
        public ReactionTypeKind Type { get; init; }
    }
}
