using Newtonsoft.Json;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class ReplyMarkupSerializationTests
{
    [Theory(DisplayName = "Should serialize request poll keyboard button")]
    [InlineData(null)]
    [InlineData("regular")]
    [InlineData("quiz")]
    public void Should_Serialize_Request_Poll_Keyboard_Button(string type)
    {
        IReplyMarkup replyMarkup = new ReplyKeyboardMarkup(
            KeyboardButton.WithRequestPoll("Create a poll", type)
        );

        string serializedReplyMarkup = JsonConvert.SerializeObject(replyMarkup);

        string formattedType = string.IsNullOrEmpty(type)
            ? "{}"
            : string.Format(@"{{""type"":""{0}""}}", type);

        string expectedString = string.Format(@"""request_poll"":{0}", formattedType);

        Assert.Contains(expectedString, serializedReplyMarkup);
    }
}