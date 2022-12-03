using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class MenuButtonSerializationTests
{
    [Fact]
    public void Should_Deserialize_Menu_Button_Web_App()
    {
        var button = new
        {
            type = MenuButtonType.WebApp,
            text = "Test text",
            web_app = new { url = "https://example.com/link/to/web/app" }
        };

        string menuButtonJson = JsonConvert.SerializeObject(button, Formatting.Indented);
        MenuButton? menuButton = JsonConvert.DeserializeObject<MenuButton>(menuButtonJson);

        MenuButtonWebApp webAppButton = Assert.IsType<MenuButtonWebApp>(menuButton);

        Assert.Equal(MenuButtonType.WebApp, menuButton.Type);
        Assert.NotNull(webAppButton.Text);
        Assert.Equal("Test text", webAppButton.Text);
        Assert.NotNull(webAppButton.WebApp);
        Assert.NotNull(webAppButton.WebApp.Url);
        Assert.Equal("https://example.com/link/to/web/app", webAppButton.WebApp.Url);
    }

    [Fact]
    public void Should_Serialize_Menu_Button_Web_App()
    {
        MenuButtonWebApp webAppButton = new()
        {
            WebApp = new() { Url = "https://example.com/link/to/web/app" },
            Text = "Test text"
        };

        string webAppButtonJson = JsonConvert.SerializeObject(webAppButton);
        Assert.Contains(@"""type"":""web_app""", webAppButtonJson);
        Assert.Contains(@"""text"":""Test text""", webAppButtonJson);
        Assert.Contains(@"""web_app"":{", webAppButtonJson);
        Assert.Contains(@"""url"":""https://example.com/link/to/web/app""", webAppButtonJson);
    }

    [Fact]
    public void Should_Deserialize_Menu_Button_Default()
    {
        var button = new { type = MenuButtonType.Default, };

        string menuButtonJson = JsonConvert.SerializeObject(button, Formatting.Indented);
        MenuButton? menuButton = JsonConvert.DeserializeObject<MenuButton>(menuButtonJson);

        Assert.NotNull(menuButton);
        Assert.Equal(MenuButtonType.Default, menuButton.Type);
        Assert.IsType<MenuButtonDefault>(menuButton);
    }

    [Fact]
    public void Should_Serialize_Menu_Button_Default()
    {
        MenuButtonDefault menuButton = new();

        string menuButtonJson = JsonConvert.SerializeObject(menuButton);
        Assert.Contains(@"""type"":""default""", menuButtonJson);
    }

    [Fact]
    public void Should_Deserialize_Menu_Button_Commands()
    {
        var button = new { type = MenuButtonType.Commands, };

        string menuButtonJson = JsonConvert.SerializeObject(button, Formatting.Indented);
        MenuButton? menuButton = JsonConvert.DeserializeObject<MenuButton>(menuButtonJson);

        Assert.NotNull(menuButton);
        Assert.Equal(MenuButtonType.Commands, menuButton.Type);
        Assert.IsType<MenuButtonCommands>(menuButton);
    }

    [Fact]
    public void Should_Serialize_Menu_Button_Commands()
    {
        MenuButtonCommands menuButton = new();

        string menuButtonJson = JsonConvert.SerializeObject(menuButton);
        Assert.Contains(@"""type"":""commands""", menuButtonJson);
    }
}
