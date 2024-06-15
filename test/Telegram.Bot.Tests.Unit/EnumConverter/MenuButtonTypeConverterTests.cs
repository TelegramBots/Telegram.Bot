using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class MenuButtonTypeConverterTests
{
    [Theory]
    [InlineData(MenuButtonType.Default, "default")]
    [InlineData(MenuButtonType.Commands, "commands")]
    [InlineData(MenuButtonType.WebApp, "web_app")]
    public void Should_Convert_MenuButtonType_To_String(MenuButtonType menuButtonType, string value)
    {
        MenuButton menuButton = new() { Type = menuButtonType };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonSerializer.Serialize(menuButton, JsonSerializerOptionsProvider.Options);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(MenuButtonType.Default, "default")]
    [InlineData(MenuButtonType.Commands, "commands")]
    [InlineData(MenuButtonType.WebApp, "web_app")]
    public void Should_Convert_String_To_MenuButtonType(MenuButtonType menuButtonType, string value)
    {
        MenuButton expectedResult = new() { Type = menuButtonType };
        string jsonData = @$"{{""type"":""{value}""}}";

        MenuButton? result = JsonSerializer.Deserialize<MenuButton>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_MenuButtonType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        MenuButton? result = JsonSerializer.Deserialize<MenuButton>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal((MenuButtonType)0, result.Type);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_MenuButtonType()
    {
        MenuButton menuButton = new() { Type = (MenuButtonType)int.MaxValue };

        // ToDo: add InputMediaType.Unknown ?
        //    protected override string GetStringValue(InputMediaType value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(menuButton, JsonSerializerOptionsProvider.Options));
    }


    class MenuButton
    {
        [JsonRequired]
        public MenuButtonType Type { get; init; }
    }
}
