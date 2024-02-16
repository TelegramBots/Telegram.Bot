using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class ChatSerializationTests
{
    [Fact]
    public void Should_Deserialize_Chat()
    {
        const string chat =
            """
            {
              "id": 12345,
              "type": "supergroup",
              "unrestrict_boost_count": 10,
              "custom_emoji_sticker_set_name": "test_sticker_set"
            }
            """;

        Chat? deserialize = JsonConvert.DeserializeObject<Chat>(chat);

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

        string json = JsonConvert.SerializeObject(chat);

        JObject j = JObject.Parse(json);

        Assert.Equal(4, j.Children().Count());
        Assert.Equal(chat.UnrestrictBoostCount, j["unrestrict_boost_count"]);
        Assert.Equal("supergroup", j["type"]);
        Assert.Equal(chat.Id, j["id"]);
        Assert.Equal(chat.CustomEmojiStickerSetName, j["custom_emoji_sticker_set_name"]);
    }
}
