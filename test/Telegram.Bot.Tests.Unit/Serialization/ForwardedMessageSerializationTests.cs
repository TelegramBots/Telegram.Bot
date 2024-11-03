using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class ForwardedMessageSerializationTests
{
    [Fact]
    public void Should_Return_MessageOriginUser_Info_From_Obsolete_Forward_Properties()
    {
        // language=JSON
        const string json = """
        {
            "message_id": 1234,
            "text": "test",
            "from": {
                "id": 1234567,
                "is_bot": false,
                "first_name": "Telegram_Bots",
                "last_name": null,
                "username": "TelegramBots",
                "language_code": null
            },
            "chat": {
                "id": 1234567,
                "first_name": "Telegram_Bots",
                "last_name": null,
                "username": "TelegramBots",
                "type": "private"
            },
            "date": 1526315997,
            "forward_origin": {
                "type": "user",
                "date": 1526315997,
                "sender_user": {
                    "id": 7654321,
                    "is_bot": false,
                    "first_name": "First Name"
                }
            }
        }
        """;

        Message? message = JsonSerializer.Deserialize<Message>(json, JsonBotAPI.Options);

        Assert.NotNull(message);
        Assert.Equal(MessageType.Text, message.Type);
        Assert.NotNull(message.ForwardOrigin);
        MessageOriginUser originUser = Assert.IsAssignableFrom<MessageOriginUser>(message.ForwardOrigin);

        Assert.Equal(new(2018, 5, 14, 16, 39, 57, DateTimeKind.Utc), originUser.Date);
        Assert.Equal(originUser.Date, message.ForwardDate);
        Assert.NotNull(message.ForwardFrom);
        Assert.Equal("First Name", message.ForwardFrom.FirstName);
        Assert.Equal(7654321, message.ForwardFrom.Id);
        Assert.False(message.ForwardFrom.IsBot);
        Assert.Equal(originUser.SenderUser.FirstName, message.ForwardFrom.FirstName);
        Assert.Equal(originUser.SenderUser.Id, message.ForwardFrom.Id);
        Assert.Equal(originUser.SenderUser.IsBot, message.ForwardFrom.IsBot);
        Assert.Null(message.ForwardSignature);
        Assert.Null(message.ForwardFromChat);
        Assert.Null(message.ForwardSenderName);
        Assert.Null(message.ForwardFromMessageId);
    }

    [Fact]
    public void Should_Return_MessageOriginHiddenUser_Info_From_Obsolete_Forward_Properties()
    {
        // language=JSON
        const string json =
            """
            {
                "message_id": 1234,
                "text": "test",
                "from": {
                    "id": 1234567,
                    "is_bot": false,
                    "first_name": "Telegram_Bots",
                    "last_name": null,
                    "username": "TelegramBots",
                    "language_code": null
                },
                "chat": {
                    "id": 1234567,
                    "first_name": "Telegram_Bots",
                    "last_name": null,
                    "username": "TelegramBots",
                    "type": "private"
                },
                "date": 1526315997,
                "forward_origin": {
                  "type": "hidden_user",
                    "date": 1526315997,
                    "sender_user_name": "Test username"

                }
            }
            """;

        Message? message = JsonSerializer.Deserialize<Message>(json, JsonBotAPI.Options);

        Assert.NotNull(message);
        Assert.Equal(MessageType.Text, message.Type);
        Assert.NotNull(message.ForwardOrigin);
        MessageOriginHiddenUser hiddenUser = Assert.IsAssignableFrom<MessageOriginHiddenUser>(message.ForwardOrigin);

        Assert.Equal(new(2018, 5, 14, 16, 39, 57, DateTimeKind.Utc), hiddenUser.Date);
        Assert.Equal(new(2018, 5, 14, 16, 39, 57, DateTimeKind.Utc), message.ForwardDate);
        Assert.Equal("Test username", message.ForwardSenderName);
        Assert.Equal(hiddenUser.Date, message.ForwardDate);
        Assert.Equal(hiddenUser.SenderUserName, message.ForwardSenderName);
        Assert.Null(message.ForwardSignature);
        Assert.Null(message.ForwardFromChat);
        Assert.Null(message.ForwardFrom);
        Assert.Null(message.ForwardFromMessageId);
    }

    [Fact]
    public void Should_Return_MessageOriginChat_Info_From_Obsolete_Forward_Properties()
    {
        // language=JSON
        const string json =
            """
            {
                "message_id": 1234,
                "text": "test",
                "from": {
                    "id": 1234567,
                    "is_bot": false,
                    "first_name": "Telegram_Bots",
                    "last_name": null,
                    "username": "TelegramBots",
                    "language_code": null
                },
                "chat": {
                    "id": 1234567,
                    "first_name": "Telegram_Bots",
                    "last_name": null,
                    "username": "TelegramBots",
                    "type": "private"
                },
                "date": 1526315997,
                "forward_origin": {
                  "type": "chat",
                    "date": 1526315997,
                    "sender_chat": {
                        "id": 7654321,
                        "first_name": "Test chat",
                        "username": "Test username",
                        "type": "supergroup"
                    },
                    "author_signature": "Test signature"
                }
            }
            """;

        Message? message = JsonSerializer.Deserialize<Message>(json, JsonBotAPI.Options);

        Assert.NotNull(message);
        Assert.Equal(MessageType.Text, message.Type);
        Assert.NotNull(message.ForwardOrigin);
        MessageOriginChat originChat = Assert.IsAssignableFrom<MessageOriginChat>(message.ForwardOrigin);

        Assert.Equal(new(2018, 5, 14, 16, 39, 57, DateTimeKind.Utc), originChat.Date);
        Assert.Equal(originChat.Date, message.ForwardDate);
        Assert.NotNull(message.ForwardFromChat);
        Assert.Equal(7654321, message.ForwardFromChat.Id);
        Assert.Equal("Test chat", message.ForwardFromChat.FirstName);
        Assert.Equal("Test username", message.ForwardFromChat.Username);
        Assert.Equal(ChatType.Supergroup, message.ForwardFromChat.Type);
        Assert.Equal(originChat.SenderChat.Id, message.ForwardFromChat.Id);
        Assert.Equal(originChat.SenderChat.FirstName, message.ForwardFromChat.FirstName);
        Assert.Equal(originChat.SenderChat.Username, message.ForwardFromChat.Username);
        Assert.Equal(originChat.SenderChat.Type, message.ForwardFromChat.Type);
        Assert.Null(message.ForwardFrom);
        Assert.Null(message.ForwardFromMessageId);
        Assert.Null(message.ForwardSenderName);
    }

    [Fact]
    public void Should_Return_MessageOriginChannel_Info_From_Obsolete_Forward_Properties()
    {
        // language=JSON
        const string json =
            """
            {
                "message_id": 1234,
                "text": "test",
                "from": {
                    "id": 1234567,
                    "is_bot": false,
                    "first_name": "Telegram_Bots",
                    "last_name": null,
                    "username": "TelegramBots",
                    "language_code": null
                },
                "chat": {
                    "id": 1234567,
                    "first_name": "Telegram_Bots",
                    "last_name": null,
                    "username": "TelegramBots",
                    "type": "private"
                },
                "date": 1526315997,
                "forward_origin": {
                  "type": "channel",
                  "date": 1526315997,
                  "chat": {
                    "id": 7654321,
                    "first_name": "Test chat",
                    "username": "Test username",
                    "type": "supergroup"
                  },
                  "message_id": 1004,
                  "author_signature": "Test signature"
                }
            }
            """;

        Message? message = JsonSerializer.Deserialize<Message>(json, JsonBotAPI.Options);

        Assert.NotNull(message);
        Assert.Equal(MessageType.Text, message.Type);
        Assert.NotNull(message.ForwardOrigin);
        MessageOriginChannel originChannel = Assert.IsAssignableFrom<MessageOriginChannel>(message.ForwardOrigin);

        Assert.Equal(new(2018, 5, 14, 16, 39, 57, DateTimeKind.Utc), originChannel.Date);
        Assert.Equal(originChannel.Date, message.ForwardDate);
        Assert.Equal(1004, message.ForwardFromMessageId);
        Assert.Equal(originChannel.MessageId, message.ForwardFromMessageId);
        Assert.NotNull(message.ForwardFromChat);
        Assert.Equal(7654321, message.ForwardFromChat.Id);
        Assert.Equal("Test signature", message.ForwardSignature);
        Assert.Equal("Test chat", message.ForwardFromChat.FirstName);
        Assert.Equal("Test username", message.ForwardFromChat.Username);
        Assert.Equal(ChatType.Supergroup, message.ForwardFromChat.Type);
        Assert.Equal(originChannel.Chat.Id, message.ForwardFromChat.Id);
        Assert.Equal(originChannel.Chat.FirstName, message.ForwardFromChat.FirstName);
        Assert.Equal(originChannel.Chat.Username, message.ForwardFromChat.Username);
        Assert.Equal(originChannel.Chat.Type, message.ForwardFromChat.Type);
        Assert.Equal(originChannel.AuthorSignature, message.ForwardSignature);
        Assert.Null(message.ForwardSenderName);
        Assert.Null(message.ForwardFrom);
    }

    [Fact]
    public void Should_Ignore_Obsolete_Forward_Properties()
    {
        Message message = new()
        {
            Id = 1,
            Text = "test",
            Date = new(2024, 1, 2, 14, 30, 0, DateTimeKind.Utc),
            Chat = new()
            {
                Id = 1234,
                Type = ChatType.Private,
            },
            ForwardOrigin = new MessageOriginUser
            {
                Date = new(2024, 4, 3, 14, 30, 0, DateTimeKind.Utc),
                SenderUser = new()
                {
                    Id = 134,
                    IsBot = false,
                    FirstName = "First name",
                },
            },
        };

        string serializedMessage = JsonSerializer.Serialize(message, JsonBotAPI.Options);
        JsonNode? root = JsonNode.Parse(serializedMessage);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.False(j.ContainsKey("forward_from"));
        Assert.False(j.ContainsKey("forward_date"));
        Assert.False(j.ContainsKey("forward_from_chat"));
        Assert.False(j.ContainsKey("forward_signature"));
        Assert.False(j.ContainsKey("forward_sender_name"));
        Assert.False(j.ContainsKey("forward_sender_name"));
    }

    [Fact]
    public void Should_Return_MessageOriginChat_Info_From_Obsolete_Forward_Properties2()
    {
        // language=JSON
        const string json =
            """
            {
              "update_id": 123,
              "chat_member": {
                "chat": {
                  "id": 1234567,
                  "first_name": "Telegram_Bots",
                  "last_name": null,
                  "username": "TelegramBots",
                  "type": "private"
                },
                "from": {
                  "id": 1234567,
                  "is_bot": false,
                  "first_name": "Telegram_Bots",
                  "last_name": null,
                  "username": "TelegramBots",
                  "language_code": null
                },
                "date": 1526315997,
                "old_chat_member": {
                  "status": "kicked",
                  "until_date": 1617321600,
                  "user": {
                    "id": 12345,
                    "is_bot": true,
                    "first_name": "First Name",
                    "last_name": "Last Name",
                    "username": "test_bot",
                    "language_code": "en_US"
                  }
                },
                "new_chat_member": {
                  "status": "kicked",
                  "until_date": 1617321600,
                  "user": {
                    "id": 12345,
                    "is_bot": true,
                    "first_name": "First Name",
                    "last_name": "Last Name",
                    "username": "test_bot",
                    "language_code": "en_US"
                  }
                }
              }
            }
            """;

        Update? update = JsonSerializer.Deserialize<Update>(json, JsonBotAPI.Options);

        Assert.NotNull(update);
        Assert.Equal(UpdateType.ChatMember, update.Type);
        Assert.NotNull(update.ChatMember);
        Assert.IsAssignableFrom<ChatMemberBanned>(update.ChatMember.OldChatMember);
        Assert.IsAssignableFrom<ChatMemberBanned>(update.ChatMember.NewChatMember);
    }
}
