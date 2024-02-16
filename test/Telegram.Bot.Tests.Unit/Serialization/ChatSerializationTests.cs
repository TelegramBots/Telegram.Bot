using Newtonsoft.Json;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class ChatSerializationTests
{
    [Fact]
    public void Should_Deserialize_Chat()
    {
        Chat chat = new()
        {
            UnrestrictBoostCount = 10,
            CustomEmojiStickerSetName = "test_sticker_set"
        };

        string serializeChat = JsonConvert.SerializeObject(chat);

        Assert.Contains(@"""unrestricted_boost_count"":10", serializeChat);
        Assert.Contains(@"""custom_emoji_sticker_set_name"":""test_sticker_set""", serializeChat);
    }
}
