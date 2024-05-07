using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class MaybeInaccessibleMessageSerializationTests
{
    [Fact]
    public void Should_Deserialize_InaccessibleMessage()
    {
        string inaccessibleMessage = """{"chat":{"id":1234,"type":"private"},"message_id":1234,"date":0}""";

        MaybeInaccessibleMessage? maybeInaccessibleMessage = JsonSerializer.Deserialize(inaccessibleMessage, TelegramBotClientJsonSerializerContext.Instance.MaybeInaccessibleMessage);

        Assert.NotNull(maybeInaccessibleMessage);
        Assert.IsType<InaccessibleMessage>(maybeInaccessibleMessage);
    }

    [Fact]
    public void Should_Serialize_InaccessibleMessage()
    {
        string expectedResult = """{"chat":{"id":1234,"type":"private"},"message_id":1234,"date":0}""";

        InaccessibleMessage inaccessibleMessage = new()
        {
            Chat = new Chat
            {
                Id = 1234,
                Type = ChatType.Private,
            },
            MessageId = 1234,
            Date = 0
        };

        string inaccessibleMessageJson = JsonSerializer.Serialize(inaccessibleMessage, TelegramBotClientJsonSerializerContext.Instance.MaybeInaccessibleMessage);
        Assert.Equal(expectedResult, inaccessibleMessageJson);
    }

    [Fact]
    public void Should_Deserialize_Message()
    {
        string message = """{"message_id":1234,"date":1704052800,"chat":{"id":1234,"type":"private"}}""";

        MaybeInaccessibleMessage? maybeInaccessibleMessage = JsonSerializer.Deserialize(message, TelegramBotClientJsonSerializerContext.Instance.MaybeInaccessibleMessage);

        Assert.NotNull(maybeInaccessibleMessage);
        Assert.IsType<Message>(maybeInaccessibleMessage);

    }

    [Fact]
    public void Should_Serialize_Message()
    {
        string expectedResult = """{"message_id":1234,"date":1704052800,"chat":{"id":1234,"type":"private"}}""";

        Message message = new()
        {
            Chat = new Chat
            {
                Id = 1234,
                Type = ChatType.Private,
            },
            MessageId = 1234,
            Date = new DateTime(2024,1,1)
        };

        string messageJson = JsonSerializer.Serialize(message, TelegramBotClientJsonSerializerContext.Instance.MaybeInaccessibleMessage);
        Assert.Equal(expectedResult, messageJson);
    }
}
