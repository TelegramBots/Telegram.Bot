using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        string json = JsonConvert.SerializeObject(chat);
        JObject j = JObject.Parse(json);

        Assert.Single(j.Children());
        Assert.Equal(101, j["boost_count"]);
    }
}
