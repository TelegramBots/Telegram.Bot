using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class ReplyMarkupSerializationTests
{
    [Theory(DisplayName = "Should serialize request poll keyboard button")]
    [InlineData(null, null)]
    [InlineData(PollType.Regular, "regular")]
    [InlineData(PollType.Quiz, "quiz")]
    public void Should_Serialize_Request_Poll_Keyboard_Button_From_Interface(PollType? pollType, string? type)
    {
        ReplyMarkup replyMarkup = new ReplyKeyboardMarkup(
            KeyboardButton.WithRequestPoll("Create a poll", pollType)
        );

        string serializedReplyMarkup = JsonSerializer.Serialize(replyMarkup, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(serializedReplyMarkup);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Single(j);

        JsonNode? keyboardNode = j["keyboard"];
        Assert.NotNull(keyboardNode);
        JsonArray jKeyboard = Assert.IsAssignableFrom<JsonArray>(keyboardNode);
        Assert.Single(jKeyboard);

        JsonNode? jRow = jKeyboard[0];
        Assert.NotNull(jRow);
        JsonArray rowNode = Assert.IsAssignableFrom<JsonArray>(jRow);
        Assert.Single(rowNode);

        JsonNode? jButton = jRow[0];
        Assert.NotNull(jButton);
        JsonObject buttonNode = Assert.IsAssignableFrom<JsonObject>(jButton);
        Assert.Equal(2, buttonNode.Count);
        Assert.Equal("Create a poll", (string?)buttonNode["text"]);

        JsonNode? jRequestPoll = buttonNode["request_poll"];
        Assert.NotNull(jRequestPoll);

        if (string.IsNullOrEmpty(type))
        {
            JsonObject requestPollNode = Assert.IsAssignableFrom<JsonObject>(jRequestPoll);
            Assert.Empty(requestPollNode);
        }
        else
        {
            JsonObject requestPollNode = Assert.IsAssignableFrom<JsonObject>(jRequestPoll);
            Assert.Single(requestPollNode);
            Assert.Equal(type, (string?)jRequestPoll["type"]);
        }
    }

    [Theory(DisplayName = "Should serialize request poll keyboard button")]
    [InlineData(null, null)]
    [InlineData(PollType.Regular, "regular")]
    [InlineData(PollType.Quiz, "quiz")]
    public void Should_Serialize_Request_Poll_Keyboard_Button(PollType? pollType, string? type)
    {
        ReplyKeyboardMarkup replyMarkup = new(
            KeyboardButton.WithRequestPoll("Create a poll", pollType)
        );

        string serializedReplyMarkup = JsonSerializer.Serialize(replyMarkup, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(serializedReplyMarkup);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Single(j);

        JsonNode? keyboardNode = j["keyboard"];
        Assert.NotNull(keyboardNode);
        JsonArray jKeyboard = Assert.IsAssignableFrom<JsonArray>(keyboardNode);
        Assert.Single(jKeyboard);

        JsonNode? jRow = jKeyboard[0];
        Assert.NotNull(jRow);
        JsonArray rowNode = Assert.IsAssignableFrom<JsonArray>(jRow);
        Assert.Single(rowNode);

        JsonNode? jButton = jRow[0];
        Assert.NotNull(jButton);
        JsonObject buttonNode = Assert.IsAssignableFrom<JsonObject>(jButton);
        Assert.Equal(2, buttonNode.Count);
        Assert.Equal("Create a poll", (string?)buttonNode["text"]);

        JsonNode? jRequestPoll = buttonNode["request_poll"];
        Assert.NotNull(jRequestPoll);

        if (string.IsNullOrEmpty(type))
        {
            JsonObject requestPollNode = Assert.IsAssignableFrom<JsonObject>(jRequestPoll);
            Assert.Empty(requestPollNode);
        }
        else
        {
            JsonObject requestPollNode = Assert.IsAssignableFrom<JsonObject>(jRequestPoll);
            Assert.Single(requestPollNode);
            Assert.Equal(type, (string?)jRequestPoll["type"]);
        }
    }
}
