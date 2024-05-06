using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class ChatBoostAddedSerializationTests
{
    [Fact]
    public void Should_Deserialize_ChatBoostAdded()
    {
        // language=JSON
        const string chatBoostAdded =
            """
            {
              "boost_count": 101
            }
            """;

        ChatBoostAdded? deserialize =
            JsonSerializer.Deserialize(chatBoostAdded, TelegramBotClientJsonSerializerContext.Instance.ChatBoostAdded);

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

        string json = JsonSerializer.Serialize(chat, TelegramBotClientJsonSerializerContext.Instance.ChatBoostAdded);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Single(j);
        Assert.Equal(101, (long?)j["boost_count"]);
    }
}
