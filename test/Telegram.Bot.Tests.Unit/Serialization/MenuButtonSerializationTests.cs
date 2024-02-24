using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class MenuButtonSerializationTests
{
    [Fact]
    public void Should_Deserialize_Menu_Button_Web_App()
    {
        const string button = """
        {
            "type": "web_app",
            "text": "Test text",
            "web_app": {
                "url": "https://example.com/link/to/web/app"
            }
        }
        """;

        MenuButton? menuButton = JsonConvert.DeserializeObject<MenuButton>(button);

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
            WebApp = new(url: "https://example.com/link/to/web/app"),
            Text = "Test text"
        };

        string webAppButtonJson = JsonConvert.SerializeObject(webAppButton);
        JObject j = JObject.Parse(webAppButtonJson);

        Assert.Equal(3, j.Children().Count());
        Assert.Equal("web_app", j["type"]);
        Assert.Equal("Test text", j["text"]);

        JToken? jWebApp = j["web_app"];
        Assert.NotNull(jWebApp);
        Assert.Single(jWebApp);
        Assert.Equal("https://example.com/link/to/web/app", jWebApp["url"]);
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
        JObject j = JObject.Parse(menuButtonJson);
        Assert.Single(j);
        Assert.Equal("default", j["type"]);
    }

    [Fact]
    public void Should_Deserialize_Menu_Button_Commands()
    {
        var button = new { type = MenuButtonType.Commands, };

        string menuButtonJson = JsonConvert.SerializeObject(button);
        JObject j = JObject.Parse(menuButtonJson);

        Assert.Single(j);
        Assert.Equal("commands", j["type"]);
    }

    [Fact]
    public void Should_Serialize_Menu_Button_Commands()
    {
        MenuButtonCommands menuButton = new();

        string menuButtonJson = JsonConvert.SerializeObject(menuButton);
        JObject j = JObject.Parse(menuButtonJson);

        Assert.Single(j);
        Assert.Equal("commands", j["type"]);
    }
}
