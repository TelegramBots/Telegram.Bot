using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

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

        string serializeStory = JsonConvert.SerializeObject(story);

        JObject j = JObject.Parse(serializeStory);

        Assert.Equal(2, j.Children().Count());
        Assert.Equal(1234, j["id"]);

        JToken? jc = j["chat"];
        Assert.NotNull(jc);

        Assert.Equal(3, jc.Children().Count());
        Assert.Equal(876543, jc["id"]);
        Assert.Equal("private", jc["type"]);
        Assert.Equal("test_user", jc["username"]);
    }

    [Fact]
    public void Should_Deserialize_Story()
    {
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

        Story? deserializedStory = JsonConvert.DeserializeObject<Story>(story);

        Assert.NotNull(deserializedStory);
        Assert.Equal(1234, deserializedStory.Id);
        Assert.Equal(876543, deserializedStory.Chat.Id);
        Assert.Equal(ChatType.Private, deserializedStory.Chat.Type);
        Assert.Equal("test_user", deserializedStory.Chat.Username);
    }
}
