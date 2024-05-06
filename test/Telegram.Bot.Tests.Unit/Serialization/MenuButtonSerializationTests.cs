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

        MenuButton? menuButton = JsonSerializer.Deserialize(button, TelegramBotClientJsonSerializerContext.Instance.MenuButton);

        MenuButtonWebApp webAppButton = Assert.IsAssignableFrom<MenuButtonWebApp>(menuButton);

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

        string webAppButtonJson = JsonSerializer.Serialize(webAppButton, TelegramBotClientJsonSerializerContext.Instance.MenuButtonWebApp);

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

        string menuButtonJson = JsonSerializer.Serialize(button);
        MenuButton? menuButton = JsonSerializer.Deserialize(menuButtonJson, TelegramBotClientJsonSerializerContext.Instance.MenuButton);

        Assert.NotNull(menuButton);
        Assert.Equal(MenuButtonType.Default, menuButton.Type);
        Assert.IsAssignableFrom<MenuButtonDefault>(menuButton);
    }

    [Fact]
    public void Should_Serialize_Menu_Button_Default()
    {
        MenuButtonDefault menuButton = new();

        string menuButtonJson = JsonSerializer.Serialize(menuButton, TelegramBotClientJsonSerializerContext.Instance.MenuButtonDefault);
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

        string menuButtonJson = JsonSerializer.Serialize(button);
        MenuButton? menuButton = JsonSerializer.Deserialize(menuButtonJson, TelegramBotClientJsonSerializerContext.Instance.MenuButton);

        Assert.NotNull(menuButton);
        Assert.Equal(MenuButtonType.Commands, menuButton.Type);
        Assert.IsAssignableFrom<MenuButtonCommands>(menuButton);
    }

    [Fact]
    public void Should_Serialize_Menu_Button_Commands()
    {
        MenuButtonCommands menuButton = new();

        string menuButtonJson = JsonSerializer.Serialize(menuButton, TelegramBotClientJsonSerializerContext.Instance.MenuButtonCommands);
        JsonNode? root = JsonNode.Parse(menuButtonJson);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Single(j);
        Assert.Equal("commands", (string?)j["type"]);
    }
}
