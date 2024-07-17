using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class InputMessageContentSerializationTests
{
    [Fact]
    public void Should_Serialize_InputTextMessageContent()
    {
        InputTextMessageContent content = new()
        {
            MessageText = "Test message @username",
            ParseMode = ParseMode.Markdown,
            Entities = [
                new() { Offset = 0, Length = 4, Type = MessageEntityType.Code },
                new() { Offset = 13, Length = 9, Type = MessageEntityType.Mention },
            ],
            LinkPreviewOptions = new()
            {
                PreferSmallMedia = true,
                ShowAboveText = true,
            }
        };

        string json = JsonSerializer.Serialize(content, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(4, j.Count);
        Assert.Equal("Test message @username", (string?)j["message_text"]);
        Assert.Equal("Markdown", (string?)j["parse_mode"]);
        Assert.NotNull(j["link_preview_options"]);
        Assert.Equal(true, (bool?)j["link_preview_options"]!["prefer_small_media"]);
        Assert.Equal(true, (bool?)j["link_preview_options"]!["show_above_text"]);
        JsonArray jEntities = Assert.IsAssignableFrom<JsonArray>(j["entities"]);
        Assert.Equal(2, jEntities.Count);

        Assert.Equal(0, (int?)jEntities[0]!["offset"]);
        Assert.Equal(4, (int?)jEntities[0]!["length"]);
        Assert.Equal("code", (string?)jEntities[0]!["type"]);

        Assert.Equal(13, (int?)jEntities[1]!["offset"]);
        Assert.Equal(9, (int?)jEntities[1]!["length"]);
        Assert.Equal("mention", (string?)jEntities[1]!["type"]);
    }
}
