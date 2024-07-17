using Telegram.Bot.Types.InlineQueryResults;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class InputQueryResultSerializationTests
{
    [Fact]
    public void Should_Serialize_InputQueryResultContact()
    {
        InlineQueryResult iqr = new InlineQueryResultContact()
        {
            Id = "test-id",
            FirstName = "First Name",
            LastName = "Last Name",
            PhoneNumber = "+123456789",
        };

        string json = JsonSerializer.Serialize(iqr, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(5, j.Count);
        Assert.Equal("contact", (string?)j["type"]);
        Assert.Equal("test-id", (string?)j["id"]);
        Assert.Equal("First Name", (string?)j["first_name"]);
        Assert.Equal("Last Name", (string?)j["last_name"]);
        Assert.Equal("+123456789", (string?)j["phone_number"]);
    }
}
