using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class MenuButtonSerializationTests
{
    [Fact]
    public void Should_Deserialize_Menu_Button_Web_App()
    {
        // language=JSON
        const string button = """
        {
            "type": "web_app",
            "text": "Test text",
            "web_app": {
                "url": "https://example.com/link/to/web/app"
            }
        }
        """;

        MenuButton? menuButton = JsonSerializer.Deserialize<MenuButton>(button, JsonBotAPI.Options);

        MenuButtonWebApp webAppButton = Assert.IsAssignableFrom<MenuButtonWebApp>(menuButton);

        Assert.Equal(MenuButtonType.WebApp, menuButton!.Type);
        Assert.NotNull(webAppButton.Text);
        Assert.Equal("Test text", webAppButton.Text);
        Assert.NotNull(webAppButton.WebApp);
        Assert.NotNull(webAppButton.WebApp.Url);
        Assert.Equal("https://example.com/link/to/web/app", webAppButton.WebApp.Url);
    }

    [Fact]
    public void Should_Serialize_Menu_Button_Web_App()
    {
        MenuButton menuButton = new MenuButtonWebApp()
        {
            WebApp = new(url: "https://example.com/link/to/web/app"),
            Text = "Test text"
        };

        string webAppButtonJson = JsonSerializer.Serialize(menuButton, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(webAppButtonJson);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(3, j.Count);

        Assert.Equal("web_app", (string?)j["type"]);
        Assert.Equal("Test text", (string?)j["text"]);

        JsonObject? jWebApp = j["web_app"]?.AsObject();
        Assert.NotNull(jWebApp);
        Assert.Single(jWebApp);
        Assert.Equal("https://example.com/link/to/web/app", (string?)jWebApp["url"]);
    }

    [Fact]
    public void Should_Deserialize_Menu_Button_Default()
    {
        var button = new { type = MenuButtonType.Default, };

        string menuButtonJson = JsonSerializer.Serialize(button, JsonBotAPI.Options);
        MenuButton? menuButton = JsonSerializer.Deserialize<MenuButton>(menuButtonJson, JsonBotAPI.Options);

        Assert.NotNull(menuButton);
        Assert.Equal(MenuButtonType.Default, menuButton.Type);
        Assert.IsAssignableFrom<MenuButtonDefault>(menuButton);
    }

    [Fact]
    public void Should_Serialize_Menu_Button_Default()
    {
        MenuButton menuButton = new MenuButtonDefault();

        string menuButtonJson = JsonSerializer.Serialize(menuButton, JsonBotAPI.Options);
        JsonNode? root = JsonNode.Parse(menuButtonJson);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Single(j);
        Assert.Equal("default", (string?)j["type"]);
    }

    [Fact]
    public void Should_Deserialize_Menu_Button_Commands()
    {
        var button = new { type = MenuButtonType.Commands, };

        string menuButtonJson = JsonSerializer.Serialize(button, JsonBotAPI.Options);
        JsonNode? root = JsonNode.Parse(menuButtonJson);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Single(j);
        Assert.Equal("commands", (string?)j["type"]);
    }

    [Fact]
    public void Should_Serialize_Menu_Button_Commands()
    {
        MenuButton menuButton = new MenuButtonCommands();

        string menuButtonJson = JsonSerializer.Serialize(menuButton, JsonBotAPI.Options);
        JsonNode? root = JsonNode.Parse(menuButtonJson);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Single(j);
        Assert.Equal("commands", (string?)j["type"]);
    }
}
