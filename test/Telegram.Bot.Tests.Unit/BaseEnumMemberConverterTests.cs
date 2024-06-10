using Telegram.Bot.Serialization;
using Xunit;

namespace Telegram.Bot.Tests.Unit;

#pragma warning disable xUnit1015
// ReSharper disable NotResolvedInText

public abstract class BaseEnumMemberConverterTests<T>
    where T : Enum
{
    [Theory]
    [MemberData("TestData")]
    public void Should_Convert_Enum_Member_To_String(T enumValue, string value)
    {
        Container container = new() { Value = enumValue };
        string expectedResult = @$"{{""value"":""{value}""}}";

        string result = JsonSerializer.Serialize(container, JsonSerializerOptionsProvider.Options);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [MemberData("TestData")]
    public void Should_Convert_String_To_Enum_Member(T enumValue, string value)
    {
        Container expectedResult = new() { Value = enumValue };
        string jsonData = @$"{{""value"":""{value}""}}";

        Container? result = JsonSerializer.Deserialize<Container>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Value, result.Value);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_ChatMemberStatus()
    {
        string jsonData = @$"{{""value"":""{int.MaxValue}""}}";

        Container? result = JsonSerializer.Deserialize<Container>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal((T)(object)0, result.Value);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_ChatMemberStatus()
    {
        Container container = new() { Value = (T)(object)int.MaxValue };

        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(container, JsonSerializerOptionsProvider.Options));
    }

    class Container
    {
        [JsonRequired]
        public T? Value { get; init; }
    }
}
