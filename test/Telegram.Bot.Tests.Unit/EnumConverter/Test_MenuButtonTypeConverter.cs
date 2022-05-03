using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class Test_MenuButtonTypeConverter
{
    [Theory]
    [InlineData(MenuButtonType.Default, "default")]
    [InlineData(MenuButtonType.Commands, "commands")]
    [InlineData(MenuButtonType.WebApp, "web_app")]
    public void Sould_Convert_MenuButtonType_To_String(MenuButtonType menuButtonType, string value)
    {
        MenuButton menuButton = new MenuButton() { Type = menuButtonType };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonConvert.SerializeObject(menuButton);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(MenuButtonType.Default, "default")]
    [InlineData(MenuButtonType.Commands, "commands")]
    [InlineData(MenuButtonType.WebApp, "web_app")]
    public void Sould_Convert_String_To_MenuButtonType(MenuButtonType menuButtonType, string value)
    {
        MenuButton expectedResult = new MenuButton() { Type = menuButtonType };
        string jsonData = @$"{{""type"":""{value}""}}";

        MenuButton result = JsonConvert.DeserializeObject<MenuButton>(jsonData);

        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Sould_Return_Zero_For_Incorrect_MenuButtonType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        MenuButton result = JsonConvert.DeserializeObject<MenuButton>(jsonData);

        Assert.Equal((MenuButtonType)0, result.Type);
    }

    [Fact]
    public void Sould_Throw_NotSupportedException_For_Incorrect_MenuButtonType()
    {
        MenuButton menuButton = new MenuButton() { Type = (MenuButtonType)int.MaxValue };

        // ToDo: add InputMediaType.Unknown ?
        //    protected override string GetStringValue(InputMediaType value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        NotSupportedException ex = Assert.Throws<NotSupportedException>(() =>
            JsonConvert.SerializeObject(menuButton));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    private class MenuButton
    {
        [JsonProperty(Required = Required.Always)]
        public MenuButtonType Type { get; init; }
    }
}
