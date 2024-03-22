using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class MessageEntityTests
{
    [Fact(DisplayName = "Should deserialize message entity with phone number type")]
    public void Should_Deserialize_Message_Entity_With_Phone_Number_Type()
    {
        // language=JSON
        const string json = """
        {
            "offset": 10,
            "length": 10,
            "type": "phone_number"
        }
        """;

        MessageEntity? message = JsonSerializer.Deserialize<MessageEntity>(json, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(message);
        Assert.Equal(MessageEntityType.PhoneNumber, message.Type);
    }

    [Fact(DisplayName = "Should serialize message entity with phone number type")]
    public void Should_Serialize_Message_Entity_With_Phone_Number_Type()
    {
        MessageEntity messageEntity = new()
        {
            Length = 10,
            Offset = 10,
            Type = MessageEntityType.PhoneNumber
        };

        string json = JsonSerializer.Serialize(messageEntity, JsonSerializerOptionsProvider.Options);
        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(3, j.Count);
        Assert.Equal(10, (long?)j["length"]);
        Assert.Equal(10, (long?)j["offset"]);
        Assert.Equal("phone_number", (string?)j["type"]);
    }

    [Fact(DisplayName = "Should deserialize message entity with unknown type")]
    public void Should_Deserialize_Message_Entity_With_Unknown_Type()
    {
        // language=JSON
        const string json = """
        {
            "offset": 10,
            "length": 10,
            "type": "totally_unknown_type"
        }
        """;

        MessageEntity? message = JsonSerializer.Deserialize<MessageEntity>(json, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(message);
        Assert.Equal((MessageEntityType)0, message.Type);
    }

    [Fact(DisplayName = "Should serialize message entity with unknown type")]
    public void Should_Serialize_Message_Entity_With_Unknown_Type()
    {
        MessageEntity messageEntity = new()
        {
            Length = 10,
            Offset = 10,
            Type = 0
        };

        string json = JsonSerializer.Serialize(messageEntity, JsonSerializerOptionsProvider.Options);
        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(3, j.Count);
        Assert.Equal(10, (long?)j["length"]);
        Assert.Equal(10, (long?)j["offset"]);
        Assert.Equal("unknown", (string?)j["type"]);
    }
}
