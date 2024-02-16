using System;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class MessageOriginSerializationTests
{
    [Fact]
    public void Should_Serialize_MessageOriginUser()
    {
        MessageOriginUser origin = new()
        {
            SenderUser = new()
            {
                Id = 12345,
                IsBot = true,
                FirstName = "First Name",
                LastName = "Last Name",
                Username = "test_bot",
                LanguageCode = "en_US",
            },
            Date = new(2024, 2, 16, 18, 0, 0, 0, DateTimeKind.Utc)
        };

        MessageOrigin? messageOrigin = JsonConvert.DeserializeObject<MessageOrigin>(
            JsonConvert.SerializeObject(origin)
        );
        Assert.NotNull(messageOrigin);

        MessageOriginUser messageOriginUser = Assert.IsType<MessageOriginUser>(messageOrigin);

        Assert.Equal(origin.Date, messageOriginUser.Date);
        Assert.Equal(origin.SenderUser.Id, messageOriginUser.SenderUser.Id);
    }

    [Fact]
    public void Should_Serialize_MessageOriginHidden()
    {
        MessageOriginHiddenUser origin = new()
        {
            SenderUserName = "test_bot",
            Date = new(2024, 2, 16, 18, 0, 0, 0, DateTimeKind.Utc)
        };

        MessageOrigin? messageOrigin = JsonConvert.DeserializeObject<MessageOrigin>(
            JsonConvert.SerializeObject(origin)
        );
        Assert.NotNull(messageOrigin);

        MessageOriginHiddenUser messageOriginHiddenUser = Assert.IsType<MessageOriginHiddenUser>(messageOrigin);

        Assert.Equal(origin.Date, messageOriginHiddenUser.Date);
        Assert.Equal(origin.SenderUserName, origin.SenderUserName);
    }

    [Fact]
    public void Should_Serialize_MessageOriginChat()
    {
        MessageOriginChat origin = new()
        {
            SenderChat = new()
            {
                Id = 12345,
                Type = ChatType.Supergroup,
                Username = "test_bot",
                IsForum = true,
            },
            Date = new(2024, 2, 16, 18, 0, 0, 0, DateTimeKind.Utc)
        };

        MessageOrigin? messageOrigin = JsonConvert.DeserializeObject<MessageOrigin>(
            JsonConvert.SerializeObject(origin)
        );
        Assert.NotNull(messageOrigin);

        MessageOriginChat messageOriginChat = Assert.IsType<MessageOriginChat>(messageOrigin);

        Assert.Equal(origin.Date, messageOriginChat.Date);
        Assert.Equal(origin.SenderChat.Id, messageOriginChat.SenderChat.Id);
        Assert.Equal(origin.SenderChat.Type, messageOriginChat.SenderChat.Type);
    }

    [Fact]
    public void Should_Serialize_MessageOriginChannel()
    {
        MessageOriginChannel origin = new()
        {
            Chat = new()
            {
                Id = 12345,
                Type = ChatType.Channel,
                Username = "test_channel",
            },
            MessageId = 1236886,
            AuthorSignature = "author_name",
            Date = new(2024, 2, 16, 18, 0, 0, 0, DateTimeKind.Utc)
        };

        MessageOrigin? messageOrigin = JsonConvert.DeserializeObject<MessageOrigin>(
            JsonConvert.SerializeObject(origin)
        );
        Assert.NotNull(messageOrigin);

        MessageOriginChannel messageOriginChat = Assert.IsType<MessageOriginChannel>(messageOrigin);

        Assert.Equal(origin.Date, messageOriginChat.Date);
        Assert.Equal(origin.Chat.Id, messageOriginChat.Chat.Id);
        Assert.Equal(origin.Chat.Type, messageOriginChat.Chat.Type);
        Assert.Equal(origin.AuthorSignature, messageOriginChat.AuthorSignature);
        Assert.Equal(origin.MessageId, messageOriginChat.MessageId);

    }

    [Fact]
    public void Should_Deserialize_MessageOriginUser()
    {
        const string origin =
            """
            {
              "type": "user",
              "sender_user": {
                  "id": 12345,
                  "is_bot": true,
                  "first_name": "First Name",
                  "last_name": "Last Name",
                  "username": "test_bot",
                  "language_code": "en_US"
              },
              "date": 1708106405
            }
            """;

        MessageOrigin? messageOrigin = JsonConvert.DeserializeObject<MessageOrigin>(origin);
        Assert.NotNull(messageOrigin);

        MessageOriginUser originUser = Assert.IsType<MessageOriginUser>(messageOrigin);

        Assert.Equal(MessageOriginType.User, messageOrigin.Type);
        Assert.Equal(new(2024, 2, 16, 18, 0, 5, 0, DateTimeKind.Utc), originUser.Date);
        Assert.NotNull(originUser.SenderUser);
        Assert.Equal(12345, originUser.SenderUser.Id);
        Assert.True(originUser.SenderUser.IsBot);
        Assert.Equal("First Name", originUser.SenderUser.FirstName);
        Assert.Equal("Last Name", originUser.SenderUser.LastName);
        Assert.Equal("test_bot", originUser.SenderUser.Username);
        Assert.Equal("en_US", originUser.SenderUser.LanguageCode);
    }

    [Fact]
    public void Should_Deserialize_MessageOriginHidden()
    {
        const string origin =
            """
            {
                "type": "hidden_user",
                "sender_user_name": "test_bot",
                "date": 1708106405
            }
            """;

        MessageOrigin? messageOrigin = JsonConvert.DeserializeObject<MessageOrigin>(origin);
        Assert.NotNull(messageOrigin);

        MessageOriginHiddenUser originHiddenUser = Assert.IsType<MessageOriginHiddenUser>(messageOrigin);

        Assert.Equal(MessageOriginType.HiddenUser, messageOrigin.Type);
        Assert.Equal(new(2024, 2, 16, 18, 0, 5, 0, DateTimeKind.Utc), originHiddenUser.Date);
        Assert.Equal("test_bot", originHiddenUser.SenderUserName);
    }

    [Fact]
    public void Should_DeSerialize_MessageOriginChat()
    {
        const string origin =
            """
            {
                "type": "chat",
                "sender_chat": {
                    "id": 12345,
                    "type": "supergroup",
                    "username": "test_bot",
                    "is_forum": true
                },
                "date": 1708106405
            }
            """;


        MessageOrigin? messageOrigin = JsonConvert.DeserializeObject<MessageOrigin>(origin);
        Assert.NotNull(messageOrigin);

        MessageOriginChat originChat = Assert.IsType<MessageOriginChat>(messageOrigin);

        Assert.Equal(MessageOriginType.Chat, messageOrigin.Type);
        Assert.Equal(new(2024, 2, 16, 18, 0, 5, 0, DateTimeKind.Utc), originChat.Date);
        Assert.NotNull(originChat.SenderChat);
        Assert.Equal(12345, originChat.SenderChat.Id);
        Assert.True(originChat.SenderChat.IsForum);
        Assert.Equal("test_bot", originChat.SenderChat.Username);
    }

    [Fact]
    public void Should_Deserialize_MessageOriginChannel()
    {
        const string origin =
            """
            {
                "type": "channel",
                "chat": {
                    "id": 12345,
                    "type": "channel",
                    "username": "test_channel",
                    "is_forum": true
                },
                "message_id": 1236886,
                "author_signature": "author_name",
                "date": 1708106405
            }
            """;

        MessageOrigin? messageOrigin = JsonConvert.DeserializeObject<MessageOrigin>(origin);
        Assert.NotNull(messageOrigin);

        MessageOriginChannel originChannel = Assert.IsType<MessageOriginChannel>(messageOrigin);

        Assert.Equal(MessageOriginType.Channel, messageOrigin.Type);
        Assert.Equal(new(2024, 2, 16, 18, 0, 5, 0, DateTimeKind.Utc), originChannel.Date);
        Assert.NotNull(originChannel.Chat);
        Assert.Equal(12345, originChannel.Chat.Id);
        Assert.True(originChannel.Chat.IsForum);
        Assert.Equal("test_channel", originChannel.Chat.Username);
        Assert.NotNull(originChannel.AuthorSignature);
        Assert.Equal("author_name", originChannel.AuthorSignature);
    }
}
