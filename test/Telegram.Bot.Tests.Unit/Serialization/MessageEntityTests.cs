using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class MessageEntityTests
{
    [Fact(DisplayName = "Should deserialize message entity with phone number type")]
    public void Should_Deserialize_Message_Entity_With_Phone_Number_Type()
    {
        const string json = """
        {
            "offset": 10,
            "length": 10,
            "type": "phone_number"
        }
        """;

        MessageEntity? message = JsonConvert.DeserializeObject<MessageEntity>(json);

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

        string json = JsonConvert.SerializeObject(messageEntity);
        JObject j = JObject.Parse(json);

        Assert.Equal(3, j.Children().Count());
        Assert.Equal(10, j["length"]);
        Assert.Equal(10, j["offset"]);
        Assert.Equal("phone_number", j["type"]);
    }

    [Fact(DisplayName = "Should deserialize message entity with unknown type")]
    public void Should_Deserialize_Message_Entity_With_Unknown_Type()
    {
        const string json = """
        {
            "offset": 10,
            "length": 10,
            "type": "totally_unknown_type"
        }
        """;

        MessageEntity? message = JsonConvert.DeserializeObject<MessageEntity>(json);

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

        string json = JsonConvert.SerializeObject(messageEntity);
        JObject j = JObject.Parse(json);

        Assert.Equal(3, j.Children().Count());
        Assert.Equal(10, j["length"]);
        Assert.Equal(10, j["offset"]);
        Assert.Equal("unknown", j["type"]);
    }
}
