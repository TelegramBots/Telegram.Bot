using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class ChatFullInfoSerializationTests
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
              "custom_emoji_sticker_set_name": "test_sticker_set",
              "accent_color_id": 123456,
              "max_reaction_count": 100
            }
            """;

        ChatFullInfo? chatFullInfo = JsonSerializer.Deserialize<ChatFullInfo>(chat, JsonBotAPI.Options);

        Assert.NotNull(chatFullInfo);
        Assert.Equal(10, chatFullInfo.UnrestrictBoostCount);
        Assert.Equal(12345, chatFullInfo.Id);
        Assert.Equal(ChatType.Supergroup, chatFullInfo.Type);
        Assert.Equal("test_sticker_set", chatFullInfo.CustomEmojiStickerSetName);
        Assert.Equal(123456, chatFullInfo.AccentColorId);
        Assert.Equal(100, chatFullInfo.MaxReactionCount);
    }

    [Fact]
    public void Should_Serialize_Chat()
    {
        ChatFullInfo chat = new()
        {
            Id = 1000,
            Type = ChatType.Supergroup,
            UnrestrictBoostCount = 10,
            CustomEmojiStickerSetName = "test_sticker_set",
            MaxReactionCount = 100,
            AccentColorId = 123456,
        };

        string json = JsonSerializer.Serialize(chat, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(chat.UnrestrictBoostCount, (int?)j["unrestrict_boost_count"]);
        Assert.Equal("supergroup", (string?)j["type"]);
        Assert.Equal(chat.Id, (long?)j["id"]);
        Assert.Equal(chat.CustomEmojiStickerSetName, (string?)j["custom_emoji_sticker_set_name"]);
        Assert.Equal(chat.AccentColorId, (int?)j["accent_color_id"]);
        Assert.Equal(chat.MaxReactionCount, (int?)j["max_reaction_count"]);
    }
}
