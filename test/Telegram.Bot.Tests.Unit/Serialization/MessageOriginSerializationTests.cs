using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

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

        string json = JsonSerializer.Serialize(origin, JsonSerializerOptionsProvider.Options);
        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(3, j.Count);
        Assert.Equal(1708106400, (long?)j["date"]);

        JsonNode? senderUserNode = j["sender_user"];
        Assert.NotNull(senderUserNode);
        JsonObject senderUser = Assert.IsAssignableFrom<JsonObject>(senderUserNode);

        Assert.Equal(6, senderUser.Count);
        Assert.Equal(12345, (long?)senderUser["id"]);
        Assert.Equal(true, (bool?)senderUser["is_bot"]);
        Assert.Equal("First Name", (string?)senderUser["first_name"]);
        Assert.Equal("Last Name", (string?)senderUser["last_name"]);
        Assert.Equal("test_bot", (string?)senderUser["username"]);
        Assert.Equal("en_US", (string?)senderUser["language_code"]);
    }

    [Fact]
    public void Should_Serialize_MessageOriginHidden()
    {
        MessageOriginHiddenUser origin = new()
        {
            SenderUserName = "test_bot",
            Date = new(2024, 2, 16, 18, 0, 0, 0, DateTimeKind.Utc)
        };

        string json = JsonSerializer.Serialize(origin, JsonSerializerOptionsProvider.Options);
        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(3, j.Count);
        Assert.Equal(1708106400, (long?)j["date"]);

        JsonNode? senderUserNode = j["sender_user_name"];
        Assert.NotNull(senderUserNode);
        JsonValue senderUser = Assert.IsAssignableFrom<JsonValue>(senderUserNode);
        Assert.NotNull(senderUser);

        Assert.Equal("test_bot", (string?)senderUserNode);
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
                Username = "test_group",
                IsForum = true,
            },
            Date = new(2024, 2, 16, 18, 0, 0, 0, DateTimeKind.Utc)
        };

        string json = JsonSerializer.Serialize(origin, JsonSerializerOptionsProvider.Options);
        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(3, j.Count);
        Assert.Equal(1708106400, (long?)j["date"]);

        JsonNode? senderChat = j["sender_chat"];
        Assert.NotNull(senderChat);
        JsonObject jsc = Assert.IsAssignableFrom<JsonObject>(senderChat);

        Assert.Equal(4, jsc.Count);
        Assert.Equal(12345, (long?)jsc["id"]);
        Assert.Equal("supergroup", (string?)jsc["type"]);
        Assert.Equal("test_group", (string?)jsc["username"]);
        Assert.Equal(true, (bool?)jsc["is_forum"]);
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

        string json = JsonSerializer.Serialize(origin, JsonSerializerOptionsProvider.Options);
        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(5, j.Count);
        Assert.Equal(1708106400, (long?)j["date"]);
        Assert.Equal(1236886, (long?)j["message_id"]);
        Assert.Equal("author_name", (string?)j["author_signature"]);

        JsonNode? chatNode = j["chat"];
        Assert.NotNull(chatNode);
        JsonObject chat = Assert.IsAssignableFrom<JsonObject>(chatNode);

        Assert.Equal(3, chat.Count);
        Assert.Equal(12345, (long?)chat["id"]);
        Assert.Equal("channel", (string?)chat["type"]);
        Assert.Equal("test_channel", (string?)chat["username"]);
    }

    [Fact]
    public void Should_Deserialize_MessageOriginUser()
    {
        // language=JSON
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

        MessageOrigin? messageOrigin = JsonSerializer.Deserialize<MessageOrigin>(origin, JsonSerializerOptionsProvider.Options);
        Assert.NotNull(messageOrigin);

        MessageOriginUser originUser = Assert.IsAssignableFrom<MessageOriginUser>(messageOrigin);

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
        // language=JSON
        const string origin =
            """
            {
                "type": "hidden_user",
                "sender_user_name": "test_bot",
                "date": 1708106405
            }
            """;

        MessageOrigin? messageOrigin = JsonSerializer.Deserialize<MessageOrigin>(origin, JsonSerializerOptionsProvider.Options);
        Assert.NotNull(messageOrigin);

        MessageOriginHiddenUser originHiddenUser = Assert.IsAssignableFrom<MessageOriginHiddenUser>(messageOrigin);

        Assert.Equal(MessageOriginType.HiddenUser, messageOrigin.Type);
        Assert.Equal(new(2024, 2, 16, 18, 0, 5, 0, DateTimeKind.Utc), originHiddenUser.Date);
        Assert.Equal("test_bot", originHiddenUser.SenderUserName);
    }

    [Fact]
    public void Should_DeSerialize_MessageOriginChat()
    {
        // language=JSON
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


        MessageOrigin? messageOrigin = JsonSerializer.Deserialize<MessageOrigin>(origin, JsonSerializerOptionsProvider.Options);
        Assert.NotNull(messageOrigin);

        MessageOriginChat originChat = Assert.IsAssignableFrom<MessageOriginChat>(messageOrigin);

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
        // language=JSON
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

        MessageOrigin? messageOrigin = JsonSerializer.Deserialize<MessageOrigin>(origin, JsonSerializerOptionsProvider.Options);
        Assert.NotNull(messageOrigin);

        MessageOriginChannel originChannel = Assert.IsAssignableFrom<MessageOriginChannel>(messageOrigin);

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
