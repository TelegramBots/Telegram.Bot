using Newtonsoft.Json;
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

        string? json = JsonConvert.SerializeObject(messageEntity);

        Assert.NotNull(json);
        Assert.True(json.Length > 10);
        Assert.Contains(@"""type"":""phone_number""", json);
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

        string? json = JsonConvert.SerializeObject(messageEntity);

        Assert.NotNull(json);
        Assert.True(json.Length > 10);
        Assert.Contains(@"""type"":""unknown""", json);
    }
}
