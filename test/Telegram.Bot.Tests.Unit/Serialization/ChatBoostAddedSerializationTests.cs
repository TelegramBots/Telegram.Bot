using Newtonsoft.Json;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class ChatBoostAddedSerializationTests
{
    [Fact]
    public void Should_Deserialize_ChatBoostAdded()
    {
        const string chatBoostAdded =
            """
            {
              "boost_count": 101
            }
            """;

        ChatBoostAdded? deserialize = JsonConvert.DeserializeObject<ChatBoostAdded>(chatBoostAdded);

        Assert.NotNull(deserialize);
        Assert.Equal(101, deserialize.BoostCount);
    }

    [Fact]
    public void Should_Serialize_ChatBoostAdded()
    {
        ChatBoostAdded chat = new()
        {
            BoostCount = 101,
        };

        ChatBoostAdded? deserialized = JsonConvert.DeserializeObject<ChatBoostAdded>(JsonConvert.SerializeObject(chat));

        Assert.NotNull(deserialized);
        Assert.Equal(deserialized.BoostCount, deserialized.BoostCount);
    }
}
