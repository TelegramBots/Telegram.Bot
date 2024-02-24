using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class ReplyMarkupSerializationTests
{
    [Theory(DisplayName = "Should serialize request poll keyboard button")]
    [InlineData(null)]
    [InlineData("regular")]
    [InlineData("quiz")]
    public void Should_Serialize_Request_Poll_Keyboard_Button(string? type)
    {
        IReplyMarkup replyMarkup = new ReplyKeyboardMarkup(
            KeyboardButton.WithRequestPoll("Create a poll", type)
        );

        string serializedReplyMarkup = JsonConvert.SerializeObject(replyMarkup);

        JObject j = JObject.Parse(serializedReplyMarkup);
        Assert.Single(j);

        JToken? jk = j["keyboard"];
        Assert.NotNull(jk);

        JArray jKeyboard = Assert.IsType<JArray>(jk);
        Assert.Single(jKeyboard);

        JToken jRow = jKeyboard[0];
        Assert.Single(jRow);

        JToken? jButton = jRow[0];
        Assert.NotNull(jButton);
        Assert.Equal(2, jButton.Children().Count());
        Assert.Equal("Create a poll", jButton["text"]);

        JToken? jRequestPoll = jButton["request_poll"];
        Assert.NotNull(jRequestPoll);

        if (string.IsNullOrEmpty(type))
        {
            Assert.Empty(jRequestPoll);
        }
        else
        {
            Assert.Single(jRequestPoll);
            Assert.Equal(type, jRequestPoll["type"]);
        }
    }
}
