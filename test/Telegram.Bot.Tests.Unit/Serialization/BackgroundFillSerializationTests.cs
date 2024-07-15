using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class BackgroundFillSerializationTests
{
    [Fact]
    public void Should_Deserialize_BackgroundFillSolid()
    {
        // language=JSON
        const string json =
            """
            {
              "type": "solid",
              "color": 123456
            }
            """;

        BackgroundFill? deserialized = JsonSerializer.Deserialize<BackgroundFill>(json, JsonBotAPI.Options);

        Assert.NotNull(deserialized);
        BackgroundFillSolid solid = Assert.IsAssignableFrom<BackgroundFillSolid>(deserialized);
        Assert.Equal(123456, solid.Color);
    }

    [Fact]
    public void Should_Serialize_BackgroundFillSolid()
    {
        BackgroundFill value = new BackgroundFillSolid()
        {
            Color = 123456,
        };

        string json = JsonSerializer.Serialize(value, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(2, j.Count);
        Assert.Equal("solid", (string?)j["type"]);
        Assert.Equal(123456, (int?)j["color"]);
    }

    [Fact]
    public void Should_Deserialize_BackgroundFillGradient()
    {
        // language=JSON
        const string json =
            """
            {
              "type": "gradient",
              "top_color": 654321,
              "bottom_color": 123456,
              "rotation_angle": 654
            }
            """;

        BackgroundFill? deserialized = JsonSerializer.Deserialize<BackgroundFill>(
            json,
            JsonBotAPI.Options
        );

        Assert.NotNull(deserialized);
        BackgroundFillGradient gradient = Assert.IsAssignableFrom<BackgroundFillGradient>(deserialized);
        Assert.Equal(123456, gradient.BottomColor);
        Assert.Equal(654321, gradient.TopColor);
        Assert.Equal(654, gradient.RotationAngle);
    }

    [Fact]
    public void Should_Serialize_BackgroundFillGradient()
    {
        BackgroundFill value = new BackgroundFillGradient()
        {
            TopColor = 123456,
            BottomColor = 654321,
            RotationAngle = 123,
        };

        string json = JsonSerializer.Serialize(value, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(4, j.Count);
        Assert.Equal("gradient", (string?)j["type"]);
        Assert.Equal(123456, (int?)j["top_color"]);
        Assert.Equal(654321, (int?)j["bottom_color"]);
        Assert.Equal(123, (int?)j["rotation_angle"]);
    }

    [Fact]
    public void Should_Deserialize_BackgroundFillFreeformGradient()
    {
        // language=JSON
        const string json =
            """
            {
              "type": "freeform_gradient",
              "colors": [123456, 654321, 987654]
            }
            """;

        BackgroundFill? deserialized = JsonSerializer.Deserialize<BackgroundFill>(
            json,
            JsonBotAPI.Options
        );

        Assert.NotNull(deserialized);
        BackgroundFillFreeformGradient freeformGradient = Assert.IsAssignableFrom<BackgroundFillFreeformGradient>(deserialized);
        Assert.NotNull(freeformGradient.Colors);
        Assert.Equal(3, freeformGradient.Colors.Length);
        Assert.Equal(123456, freeformGradient.Colors[0]);
        Assert.Equal(654321, freeformGradient.Colors[1]);
        Assert.Equal(987654, freeformGradient.Colors[2]);
    }

    [Fact]
    public void Should_Serialize_BackgroundFillFreeformGradient()
    {
        BackgroundFill value = new BackgroundFillFreeformGradient()
        {
            Colors = [123456, 654321, 987654],
        };

        string json = JsonSerializer.Serialize(value, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(2, j.Count);
        Assert.Equal("freeform_gradient", (string?)j["type"]);
        JsonArray ja = Assert.IsAssignableFrom<JsonArray>(j["colors"]);
        Assert.Equal(3, ja.Count);
        Assert.Equal(123456, (int?)ja[0]);
        Assert.Equal(654321, (int?)ja[1]);
        Assert.Equal(987654, (int?)ja[2]);
    }
}
