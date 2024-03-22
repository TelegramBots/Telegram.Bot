using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class StorySerializationTests
{
    [Fact]
    public void Should_Serialize_Story()
    {
        Story story = new()
        {
            Id = 1234,
            Chat = new()
            {
                Id = 876543,
                Type = ChatType.Private,
                Username = "test_user"
            },
        };

        string serializeStory = JsonSerializer.Serialize(story, JsonSerializerOptionsProvider.Options);

        JsonNode? root = JsonNode.Parse(serializeStory);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(2, j.Count);
        Assert.Equal(1234, (long?)j["id"]);

        JsonNode? jc = j["chat"];
        Assert.NotNull(jc);
        JsonObject chatNode = Assert.IsAssignableFrom<JsonObject>(jc);

        Assert.Equal(3, chatNode.Count);
        Assert.Equal(876543, (long?)chatNode["id"]);
        Assert.Equal("private", (string?)chatNode["type"]);
        Assert.Equal("test_user", (string?)chatNode["username"]);
    }

    [Fact]
    public void Should_Deserialize_Story()
    {
        // language=JSON
        const string story =
            """
            {
                "id": 1234,
                "chat": {
                    "id": 876543,
                    "type": "private",
                    "username": "test_user"
                }
            }
            """;

        Story? deserializedStory = JsonSerializer.Deserialize<Story>(story, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(deserializedStory);
        Assert.Equal(1234, deserializedStory.Id);
        Assert.Equal(876543, deserializedStory.Chat.Id);
        Assert.Equal(ChatType.Private, deserializedStory.Chat.Type);
        Assert.Equal("test_user", deserializedStory.Chat.Username);
    }
}
