using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class InputMediaSerializationTests
{
    [Fact]
    public void Should_Deserialize_InputMedia_To_Base_Type()
    {
        // language=JSON
        const string json = """
        {
            "type": "animation",
            "caption": "Test *caption*",
            "media": "https://example.com/image.gif",
            "has_spoiler": true,
            "width": 300,
            "height": 200
        }
        """;

        InputMedia? inputMedia = JsonSerializer.Deserialize<InputMedia>(json, JsonBotAPI.Options);

        Assert.NotNull(inputMedia);
        Assert.Equal(InputMediaType.Animation, inputMedia.Type);
        Assert.Equal("Test *caption*", inputMedia.Caption);

        InputFileUrl file = Assert.IsAssignableFrom<InputFileUrl>(inputMedia.Media);
        Assert.Equal(new("https://example.com/image.gif"), file.Url);

        InputMediaAnimation animation = Assert.IsAssignableFrom<InputMediaAnimation>(inputMedia);
        Assert.True(animation.HasSpoiler);
        Assert.Equal(200, animation.Height);
        Assert.Equal(300, animation.Width);
    }

    [Fact]
    public void Should_Deserialize_InputMedia_To_Concrete_Type()
    {
        // language=JSON
        const string json = """
        {
            "type": "animation",
            "caption": "Test *caption*",
            "media": "https://example.com/image.gif",
            "has_spoiler": true,
            "width": 300,
            "height": 200
        }
        """;

        InputMediaAnimation? animation = JsonSerializer.Deserialize<InputMediaAnimation>(json, JsonBotAPI.Options);

        Assert.NotNull(animation);
        Assert.Equal("Test *caption*", animation.Caption);

        InputFileUrl file = Assert.IsAssignableFrom<InputFileUrl>(animation.Media);
        Assert.Equal(new("https://example.com/image.gif"), file.Url);
        Assert.True(animation.HasSpoiler);
        Assert.Equal(200, animation.Height);
        Assert.Equal(300, animation.Width);
    }

    [Fact]
    public void Should_Serialize_InputMediaAnimation_From_Concrete_Type()
    {
        InputMediaAnimation animation = new()
        {
            HasSpoiler = true,
            Media = InputFile.FromString("https://example.com/image.gif"),
            Caption = "Test *caption*",
            Width = 300,
            Height = 200,
        };

        string json = JsonSerializer.Serialize(animation, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(6, j.Count);
        Assert.Equal("animation", (string?)j["type"]);
        Assert.Equal(true, (bool?)j["has_spoiler"]);
        Assert.Equal("Test *caption*", (string?)j["caption"]);
        Assert.Equal(300, (int?)j["width"]);
        Assert.Equal(200, (int?)j["height"]);
        Assert.Equal("https://example.com/image.gif", (string?)j["media"]);
    }

    [Fact]
    public void Should_Serialize_InputMediaAnimation_From_Base_Type()
    {
        InputMedia animation = new InputMediaAnimation
        {
            HasSpoiler = true,
            Media = InputFile.FromString("https://example.com/image.gif"),
            Caption = "Test *caption*",
            Width = 300,
            Height = 200,
        };

        string json = JsonSerializer.Serialize(animation, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(6, j.Count);
        Assert.Equal("animation", (string?)j["type"]);
        Assert.Equal(true, (bool?)j["has_spoiler"]);
        Assert.Equal("Test *caption*", (string?)j["caption"]);
        Assert.Equal(300, (int?)j["width"]);
        Assert.Equal(200, (int?)j["height"]);
        Assert.Equal("https://example.com/image.gif", (string?)j["media"]);
    }

    [Fact]
    public void Should_Serialize_InputMediaPhoto_From_Album_Interface()
    {
        IAlbumInputMedia animation = new InputMediaPhoto
        {
            HasSpoiler = true,
            Media = InputFile.FromString("https://example.com/image.png"),
            Caption = "Test *caption*",
        };

        string json = JsonSerializer.Serialize(animation, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(4, j.Count);
        Assert.Equal("photo", (string?)j["type"]);
        Assert.Equal(true, (bool?)j["has_spoiler"]);
        Assert.Equal("Test *caption*", (string?)j["caption"]);
        Assert.Equal("https://example.com/image.png", (string?)j["media"]);
    }
}
