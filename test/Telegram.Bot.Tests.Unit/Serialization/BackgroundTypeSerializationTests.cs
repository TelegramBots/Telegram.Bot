using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class BackgroundTypeSerializationTests
{
    [Fact]
    public void Should_Deserialize_BackgroundTypeFill()
    {
        // language=JSON
        const string json =
            """
            {
              "type": "fill",
              "dark_theme_dimming": 54,
              "fill":
              {
                "type": "solid",
                "color": 123456
              }
            }
            """;

        BackgroundType? deserialized = JsonSerializer.Deserialize<BackgroundType>(
            json,
            JsonBotAPI.Options
        );
        Assert.NotNull(deserialized);
        BackgroundTypeFill fill = Assert.IsAssignableFrom<BackgroundTypeFill>(deserialized);
        Assert.Equal(54, fill.DarkThemeDimming);
        BackgroundFillSolid fillSolid = Assert.IsAssignableFrom<BackgroundFillSolid>(fill.Fill);
        Assert.Equal(123456, fillSolid.Color);
    }

    [Fact]
    public void Should_Serialize_BackgroundTypeFill()
    {
        BackgroundType value = new BackgroundTypeFill()
        {
            DarkThemeDimming = 43,
            Fill = new BackgroundFillSolid { Color = 123456 },
        };

        string json = JsonSerializer.Serialize(value, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(3, j.Count);
        Assert.Equal("fill", (string?)j["type"]);
        Assert.Equal(43, (int?)j["dark_theme_dimming"]);
        Assert.Equal("solid", (string?)j["fill"]?["type"]);
        Assert.Equal(123456, (int?)j["fill"]?["color"]);
    }

    [Fact]
    public void Should_Deserialize_BackgroundTypeWallpaper()
    {
        // language=JSON
        const string json =
            """
            {
              "type": "wallpaper",
              "dark_theme_dimming": 54,
              "document": {
                "file_id": "AgADAgADvKgxGxW80EtRgjrTaWNmy7UerQ4ABIrxzSBLXOQYw54BAAEC",
                "file_unique_id": "AgADcOsAAhUdZAc",
                "file_size": 52433
              },
              "is_blurred": false,
              "is_moving":  true
            }
            """;

        BackgroundType? deserialized = JsonSerializer.Deserialize<BackgroundType>(
            json,
            JsonBotAPI.Options
        );

        Assert.NotNull(deserialized);
        BackgroundTypeWallpaper wallpaper = Assert.IsAssignableFrom<BackgroundTypeWallpaper>(deserialized);
        Assert.Equal(54, wallpaper.DarkThemeDimming);
        Assert.True(wallpaper.IsMoving);
        Assert.False(wallpaper.IsBlurred);
        Assert.NotNull(wallpaper.Document);
        Assert.Equal(52433, wallpaper.Document.FileSize);
        Assert.Equal(52433, wallpaper.Document.FileSize);
        Assert.Equal("AgADAgADvKgxGxW80EtRgjrTaWNmy7UerQ4ABIrxzSBLXOQYw54BAAEC", wallpaper.Document.FileId);
        Assert.Equal("AgADcOsAAhUdZAc", wallpaper.Document.FileUniqueId);
        Assert.Null(wallpaper.Document.FileName);
        Assert.Null(wallpaper.Document.Thumbnail);
        Assert.Null(wallpaper.Document.MimeType);
    }

    [Fact]
    public void Should_Serialize_BackgroundTypeWallpaper()
    {
        BackgroundType value = new BackgroundTypeWallpaper()
        {
            DarkThemeDimming = 43,
            Document = new()
            {
                FileId = "AgADAgADvKgxGxW80EtRgjrTaWNmy7UerQ4ABIrxzSBLXOQYw54BAAEC",
                FileUniqueId = "AgADcOsAAhUdZAc",
                FileSize = 52433,
            },
            IsMoving = true,
            IsBlurred = false,
        };

        string json = JsonSerializer.Serialize(value, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(4, j.Count);
        Assert.Equal("wallpaper", (string?)j["type"]);
        Assert.Equal(43, (int?)j["dark_theme_dimming"]);
        Assert.Null((bool?)j["is_blurred"]);
        Assert.Equal(true, (bool?)j["is_moving"]);
        Assert.NotNull(j["document"]);

        JsonObject jd = Assert.IsAssignableFrom<JsonObject>(j["document"]);
        Assert.Equal(3, jd.Count);
        Assert.Equal("AgADAgADvKgxGxW80EtRgjrTaWNmy7UerQ4ABIrxzSBLXOQYw54BAAEC", (string?)jd["file_id"]);
        Assert.Equal("AgADcOsAAhUdZAc", (string?)jd["file_unique_id"]);
        Assert.Equal(52433, (long?)jd["file_size"]);
    }

    [Fact]
    public void Should_Deserialize_BackgroundTypePattern()
    {
        // language=JSON
        const string json =
            """
            {
              "type": "pattern",
              "intensity": 54,
              "document": {
                "file_id": "AgADAgADvKgxGxW80EtRgjrTaWNmy7UerQ4ABIrxzSBLXOQYw54BAAEC",
                "file_unique_id": "AgADcOsAAhUdZAc",
                "file_size": 52433
              },
              "fill": {
                "type": "solid",
                "color": 123456
              },
              "is_inverted": false,
              "is_moving":  true
            }
            """;

        BackgroundType? deserialized = JsonSerializer.Deserialize<BackgroundType>(
            json,
            JsonBotAPI.Options
        );

        Assert.NotNull(deserialized);
        BackgroundTypePattern pattern = Assert.IsAssignableFrom<BackgroundTypePattern>(deserialized);
        Assert.Equal(54, pattern.Intensity);
        Assert.True(pattern.IsMoving);
        Assert.False(pattern.IsInverted);
        Assert.NotNull(pattern.Document);
        Assert.Equal(52433, pattern.Document.FileSize);
        Assert.Equal(52433, pattern.Document.FileSize);
        Assert.Equal("AgADAgADvKgxGxW80EtRgjrTaWNmy7UerQ4ABIrxzSBLXOQYw54BAAEC", pattern.Document.FileId);
        Assert.Equal("AgADcOsAAhUdZAc", pattern.Document.FileUniqueId);
        Assert.Null(pattern.Document.FileName);
        Assert.Null(pattern.Document.Thumbnail);
        Assert.Null(pattern.Document.MimeType);
        Assert.Equal(BackgroundFillType.Solid, pattern.Fill.Type);
        Assert.IsAssignableFrom<BackgroundFillSolid>(pattern.Fill);
    }

    [Fact]
    public void Should_Serialize_BackgroundTypePattern()
    {
        BackgroundType value = new BackgroundTypePattern()
        {
            Intensity = 43,
            Document = new()
            {
                FileId = "AgADAgADvKgxGxW80EtRgjrTaWNmy7UerQ4ABIrxzSBLXOQYw54BAAEC",
                FileUniqueId = "AgADcOsAAhUdZAc",
                FileSize = 52433,
            },
            IsMoving = true,
            IsInverted = false,
            Fill = new BackgroundFillSolid
            {
                Color = 123456,
            }
        };

        string json = JsonSerializer.Serialize(value, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(5, j.Count);
        Assert.Equal("pattern", (string?)j["type"]);
        Assert.Equal(43, (int?)j["intensity"]);
        Assert.Null((bool?)j["is_inverted"]);
        Assert.Equal(true, (bool?)j["is_moving"]);
        Assert.NotNull(j["document"]);

        JsonObject jd = Assert.IsAssignableFrom<JsonObject>(j["document"]);
        Assert.Equal(3, jd.Count);
        Assert.Equal("AgADAgADvKgxGxW80EtRgjrTaWNmy7UerQ4ABIrxzSBLXOQYw54BAAEC", (string?)jd["file_id"]);
        Assert.Equal("AgADcOsAAhUdZAc", (string?)jd["file_unique_id"]);
        Assert.Equal(52433, (long?)jd["file_size"]);

        JsonObject jf = Assert.IsAssignableFrom<JsonObject>(j["fill"]);
        Assert.Equal(2, jf.Count);
        Assert.Equal(123456, (int?)jf["color"]);
        Assert.Equal("solid", (string?)jf["type"]);
    }

    [Fact]
    public void Should_Deserialize_BackgroundTypeChatTheme()
    {
        // language=JSON
        const string json =
            """
            {
              "type": "chat_theme",
              "theme_name": "Test Theme Name"
            }
            """;

        BackgroundType? deserialized = JsonSerializer.Deserialize<BackgroundType>(
            json,
            JsonBotAPI.Options
        );

        BackgroundTypeChatTheme chatTheme = Assert.IsAssignableFrom<BackgroundTypeChatTheme>(deserialized);
        Assert.NotNull(chatTheme);
        Assert.Equal("Test Theme Name", chatTheme.ThemeName);
    }

    [Fact]
    public void Should_Serialize_BackgroundTypeChatTheme()
    {
        BackgroundType value = new BackgroundTypeChatTheme()
        {
            ThemeName = "Test Theme Name"
        };

        string json = JsonSerializer.Serialize(value, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(2, j.Count);
        Assert.Equal("chat_theme", (string?)j["type"]);
        Assert.Equal("Test Theme Name", (string?)j["theme_name"]);
    }
}
