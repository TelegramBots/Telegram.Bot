using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class ChatSerializationTests
{
    [Fact]
    public void Should_Deserialize_Chat()
    {
        // language=JSON
        const string chat =
            """
            {
              "id": 12345,
              "type": "supergroup",
              "unrestrict_boost_count": 10,
              "custom_emoji_sticker_set_name": "test_sticker_set"
            }
            """;

        Chat? deserialize = JsonSerializer.Deserialize<Chat>(chat, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(deserialize);
        Assert.Equal(10, deserialize.UnrestrictBoostCount);
        Assert.Equal(12345, deserialize.Id);
        Assert.Equal(ChatType.Supergroup, deserialize.Type);
        Assert.Equal("test_sticker_set", deserialize.CustomEmojiStickerSetName);
    }

    [Fact]
    public void Should_Serialize_Chat()
    {
        Chat chat = new()
        {
            Id = 1000,
            Type = ChatType.Supergroup,
            UnrestrictBoostCount = 10,
            CustomEmojiStickerSetName = "test_sticker_set"
        };

        string json = JsonSerializer.Serialize(chat, JsonSerializerOptionsProvider.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(4, j.Count);
        Assert.Equal(chat.UnrestrictBoostCount, (int?)j["unrestrict_boost_count"]);
        Assert.Equal("supergroup", (string?)j["type"]);
        Assert.Equal(chat.Id, (long?)j["id"]);
        Assert.Equal(chat.CustomEmojiStickerSetName, (string?)j["custom_emoji_sticker_set_name"]);
    }
}
