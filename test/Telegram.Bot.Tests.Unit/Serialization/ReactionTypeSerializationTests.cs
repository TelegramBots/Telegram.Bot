using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class ReactionTypeSerializationTests
{
    [Fact]
    public void Should_Deserialize_ReactionTypeEmoji()
    {
        // language=JSON
        const string json = """
        {
            "type": "emoji",
            "emoji": "😎"
        }
        """;

        ReactionType? reactionType = JsonSerializer.Deserialize<ReactionType>(json, JsonBotAPI.Options);

        ReactionTypeEmoji reactionTypeEmoji = Assert.IsAssignableFrom<ReactionTypeEmoji>(reactionType);

        Assert.Equal(ReactionTypeKind.Emoji, reactionTypeEmoji.Type);
        Assert.Equal("😎", reactionTypeEmoji.Emoji);
    }

    [Fact]
    public void Should_Deserialize_ReactionTypeCustomEmoji()
    {
        // language=JSON
        const string json = """
        {
            "type": "custom_emoji",
            "custom_emoji_id": "custom-emoji-id"
        }
        """;

        ReactionType? reactionType = JsonSerializer.Deserialize<ReactionType>(json, JsonBotAPI.Options);

        ReactionTypeCustomEmoji reactionTypeCustomEmoji = Assert.IsAssignableFrom<ReactionTypeCustomEmoji>(reactionType);

        Assert.Equal(ReactionTypeKind.CustomEmoji, reactionTypeCustomEmoji.Type);
        Assert.Equal("custom-emoji-id", reactionTypeCustomEmoji.CustomEmojiId);
    }

    [Fact]
    public void Should_Serialize_ReactionTypeEmoji()
    {
        ReactionType reactionType = new ReactionTypeEmoji()
        {
            Emoji = "😎"
        };

        string json = JsonSerializer.Serialize(reactionType, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(2, j.Count);
        Assert.Equal("😎", (string?)j["emoji"]);
        Assert.Equal("emoji", (string?)j["type"]);
    }

    [Fact]
    public void Should_Serialize_ReactionTypeCustomEmoji()
    {
        ReactionType reactionType = new ReactionTypeCustomEmoji()
        {
            CustomEmojiId = "custom-emoji-id"
        };

        string json = JsonSerializer.Serialize(reactionType, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(2, j.Count);
        Assert.Equal("custom_emoji", (string?)j["type"]);
        Assert.Equal("custom-emoji-id", (string?)j["custom_emoji_id"]);
    }

    [Fact]
    public void Should_Serialize_ReactionTypeCustomEmoji_From_Concrete_Type()
    {
        ReactionTypeCustomEmoji reactionType = new()
        {
            CustomEmojiId = "9999999999"
        };

        string json = JsonSerializer.Serialize(reactionType, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(2, j.Count);
        Assert.Equal("custom_emoji", (string?)j["type"]);
        Assert.Equal("9999999999", (string?)j["custom_emoji_id"]);
    }
}
