using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class MessageOriginTypeConverterTests
{
    [Theory]
    [ClassData(typeof(MessageOriginData))]
    public void Should_Convert_MessageOriginType_To_String(MessageOrigin messageOrigin, string value)
    {
        string result = JsonSerializer.Serialize(messageOrigin, TelegramBotClientJsonSerializerContext.Instance.MessageOrigin);

        Assert.Equal(value, result);
    }

    [Theory]
    [ClassData(typeof(MessageOriginData))]
    public void Should_Convert_String_To_MessageOriginType(MessageOrigin expectedResult, string value)
    {
        MessageOrigin? result = JsonSerializer.Deserialize(value, TelegramBotClientJsonSerializerContext.Instance.MessageOrigin);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_MessageOriginType()
    {
        MessageOriginType? result = JsonSerializer.Deserialize(int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.MessageOriginType);

        Assert.NotNull(result);
        Assert.Equal((MessageOriginType)0, result);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_MessageOriginType()
    {
        Assert.Throws<JsonException>(() =>
            JsonSerializer.Serialize((MessageOriginType)int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.MessageOriginType));
    }

    private class MessageOriginData : IEnumerable<object[]>
    {
        private static MessageOrigin NewMessageOrigin(MessageOriginType messageOriginType)
        {
            return messageOriginType switch
            {
                MessageOriginType.User => new MessageOriginUser
                {
                    Date = new DateTime(2024, 01, 01),
                    SenderUser = new User
                    {
                        Id = 123456789,
                        IsBot = false,
                        FirstName = "FirstName",
                    }
                },
                MessageOriginType.HiddenUser => new MessageOriginHiddenUser
                {
                    Date = new DateTime(2024, 01, 01),
                    SenderUserName = "a",
                },
                MessageOriginType.Chat => new MessageOriginChat
                {
                    Date = new DateTime(2024, 01, 01),
                    SenderChat = new Chat
                    {
                        Id = 1234,
                        Type = ChatType.Private,
                    }
                },
                MessageOriginType.Channel => new MessageOriginChannel
                {
                    Date = new DateTime(2024, 01, 01),
                    Chat = new Chat
                    {
                        Id = 1234,
                        Type = ChatType.Private,
                    },
                    MessageId = 1234,
                },
                _ => throw new ArgumentOutOfRangeException(nameof(messageOriginType), messageOriginType, null)
            };

        }

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewMessageOrigin(MessageOriginType.User), """{"type":"user","sender_user":{"id":123456789,"is_bot":false,"first_name":"FirstName"},"date":1704052800}"""];
            yield return [NewMessageOrigin(MessageOriginType.HiddenUser), """{"type":"hidden_user","sender_user_name":"a","date":1704052800}"""];
            yield return [NewMessageOrigin(MessageOriginType.Chat), """{"type":"chat","sender_chat":{"id":1234,"type":"private"},"date":1704052800}"""];
            yield return [NewMessageOrigin(MessageOriginType.Channel), """{"type":"channel","chat":{"id":1234,"type":"private"},"message_id":1234,"date":1704052800}"""];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
