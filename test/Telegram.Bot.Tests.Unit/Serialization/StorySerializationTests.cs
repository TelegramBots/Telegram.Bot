using Newtonsoft.Json;
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

        Story? deserializedStory = JsonConvert.DeserializeObject<Story>(JsonConvert.SerializeObject(story));

        Assert.NotNull(deserializedStory);
        Assert.Equal(story.Id, deserializedStory.Id);
        Assert.Equal(story.Chat.Id, deserializedStory.Chat.Id);
        Assert.Equal(story.Chat.Type, deserializedStory.Chat.Type);
        Assert.Equal(story.Chat.Username, deserializedStory.Chat.Username);
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
