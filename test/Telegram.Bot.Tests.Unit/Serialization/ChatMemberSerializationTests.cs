using System.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class ChatMemberSerializationTests
{
    [Fact]
    public void Should_Deserialize_Chat_Member_Member()
    {
        // language=JSON
        const string json = """
        {
            "status": "creator",
            "user": {
                "id": 12345,
                "is_bot": true,
                "first_name": "First Name",
                "last_name": "Last Name",
                "username": "test_bot",
                "language_code": "en_US"
            },
            "is_anonymous": true,
            "custom_title": "custom test title"
        }
        """;

        ChatMember? chatMember = JsonSerializer.Deserialize<ChatMember>(json, JsonSerializerOptionsProvider.Options);

        ChatMemberOwner owner = Assert.IsAssignableFrom<ChatMemberOwner>(chatMember);

        Assert.Equal(ChatMemberStatus.Creator, chatMember.Status);
        Assert.True(owner.IsAnonymous);
        Assert.Equal("custom test title", owner.CustomTitle);
        Assert.NotNull(chatMember.User);
        Assert.Equal(12345, chatMember.User.Id);
        Assert.True(chatMember.User.IsBot);
        Assert.Equal("First Name", chatMember.User.FirstName);
        Assert.Equal("Last Name", chatMember.User.LastName);
        Assert.Equal("test_bot", chatMember.User.Username);
        Assert.Equal("en_US", chatMember.User.LanguageCode);
    }

    [Fact]
    public void Should_Serialize_Chat_Member_Member()
    {
        ChatMemberOwner creator = new()
        {
            User = new()
            {
                Id = 12345,
                IsBot = true,
                FirstName = "First Name",
                LastName = "Last Name",
                Username = "test_bot",
                LanguageCode = "en_US",
            },
            IsAnonymous = true,
            CustomTitle = "Custom test title"
        };

        string chatMemberJson = JsonSerializer.Serialize(creator, JsonSerializerOptionsProvider.Options);

        JsonNode? root = JsonNode.Parse(chatMemberJson);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(4, j.Count);
        Assert.Equal("creator", (string?)j["status"]);
        Assert.Equal(true, (bool?)j["is_anonymous"]);
        Assert.Equal("Custom test title", (string?)j["custom_title"]);

        JsonNode? userNode = j["user"];
        Assert.NotNull(userNode);

        JsonObject ju = Assert.IsAssignableFrom<JsonObject>(userNode);
        Assert.Equal(6, ju.Count);
        Assert.Equal(12345, (long?)ju["id"]);
        Assert.Equal(true, (bool?)ju["is_bot"]);
        Assert.Equal("First Name", (string?)ju["first_name"]);
        Assert.Equal("Last Name", (string?)ju["last_name"]);
        Assert.Equal("test_bot", (string?)ju["username"]);
        Assert.Equal("en_US", (string?)ju["language_code"]);
    }

    [Fact]
    public void Should_Serialize_Chat_Member_Banned()
    {
        ChatMemberBanned creator = new()
        {
            User = new()
            {
                Id = 12345,
                IsBot = true,
                FirstName = "First Name",
                LastName = "Last Name",
                Username = "test_bot",
                LanguageCode = "en_US",
            },
            UntilDate = new(2021, 4, 2, 0, 0, 0, DateTimeKind.Utc)
        };

        string chatMemberJson = JsonSerializer.Serialize(creator, JsonSerializerOptionsProvider.Options);

        JsonNode? root = JsonNode.Parse(chatMemberJson);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(3, j.Count);
        Assert.Equal(1617321600, (long?)j["until_date"]);
        Assert.Equal("kicked", (string?)j["status"]);
        Assert.True(j.ContainsKey("user"));

        JsonNode? userNode = j["user"];
        Assert.NotNull(userNode);

        JsonObject ju = Assert.IsAssignableFrom<JsonObject>(userNode);
        Assert.Equal(6, ju.Count);
        Assert.Equal(12345, (long?)ju["id"]);
        Assert.Equal(true, (bool?)ju["is_bot"]);
        Assert.Equal("Last Name", (string?)ju["last_name"]);
        Assert.Equal("First Name", (string?)ju["first_name"]);
        Assert.Equal("test_bot", (string?)ju["username"]);
        Assert.Equal("en_US", (string?)ju["language_code"]);
    }

    [Fact]
    public void Should_Serialize_Chat_Member_Banned_2()
    {
        ChatMemberBanned creator = new()
        {
            User = new()
            {
                Id = 12345,
                IsBot = true,
                FirstName = "First Name",
                LastName = "Last Name",
                Username = "test_bot",
                LanguageCode = "en_US",
            },
        };

        string chatMemberJson = JsonSerializer.Serialize(creator, JsonSerializerOptionsProvider.Options);
        JsonNode? root = JsonNode.Parse(chatMemberJson);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(2, j.Count);
        Assert.False(j.ContainsKey("until_date"));
        Assert.Equal("kicked", (string?)j["status"]);

        JsonNode? userNode = j["user"];
        Assert.NotNull(userNode);

        JsonObject ju = Assert.IsAssignableFrom<JsonObject>(userNode);

        Assert.Equal(6, ju.Count);
        Assert.Equal(12345, (long?)ju["id"]);
        Assert.Equal(true, (bool?)ju["is_bot"]);
        Assert.Equal("First Name", (string?)ju["first_name"]);
        Assert.Equal("Last Name", (string?)ju["last_name"]);
        Assert.Equal("test_bot", (string?)ju["username"]);
        Assert.Equal("en_US", (string?)ju["language_code"]);
    }

    [Fact]
    public void Should_Deserialize_Chat_Member_Banned()
    {
        // language=JSON
        const string json = """
        {
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
        """;

        ChatMemberBanned? bannedUser = JsonSerializer.Deserialize<ChatMemberBanned>(json, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(bannedUser);
        Assert.Equal(ChatMemberStatus.Kicked, bannedUser.Status);
        Assert.Equal(new(2021, 4, 2, 0, 0, 0, DateTimeKind.Utc), bannedUser.UntilDate);
        Assert.NotNull(bannedUser.User);
        Assert.Equal(12345, bannedUser.User.Id);
        Assert.True(bannedUser.User.IsBot);
        Assert.Equal("First Name", bannedUser.User.FirstName);
        Assert.Equal("Last Name", bannedUser.User.LastName);
        Assert.Equal("test_bot", bannedUser.User.Username);
        Assert.Equal("en_US", bannedUser.User.LanguageCode);
    }

    [Fact]
    public void Should_Deserialize_Chat_Member_Banned_2()
    {
        // language=JSON
        const string json = """
        {
            "status": "kicked",
            "until_date": 0,
            "user": {
                "id": 12345,
                "is_bot": true,
                "first_name": "First Name",
                "last_name": "Last Name",
                "username": "test_bot",
                "language_code": "en_US"
            }
        }
        """;

        ChatMemberBanned? bannedUser = JsonSerializer.Deserialize<ChatMemberBanned>(json, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(bannedUser);
        Assert.Equal(ChatMemberStatus.Kicked, bannedUser.Status);
        Assert.Null(bannedUser.UntilDate);
        Assert.NotNull(bannedUser.User);
        Assert.Equal(12345, bannedUser.User.Id);
        Assert.True(bannedUser.User.IsBot);
        Assert.Equal("First Name", bannedUser.User.FirstName);
        Assert.Equal("Last Name", bannedUser.User.LastName);
        Assert.Equal("test_bot", bannedUser.User.Username);
        Assert.Equal("en_US", bannedUser.User.LanguageCode);
    }

    [Fact]
    public void Should_Deserialize_Chat_Member_Banned_3()
    {
        // language=JSON
        const string json = """
        {
            "status": "kicked",
            "user": {
                "id": 12345,
                "is_bot": true,
                "first_name": "First Name",
                "last_name": "Last Name",
                "username": "test_bot",
                "language_code": "en_US"
            }
        }
        """;

        ChatMemberBanned? bannedUser = JsonSerializer.Deserialize<ChatMemberBanned>(json, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(bannedUser);
        Assert.Equal(ChatMemberStatus.Kicked, bannedUser.Status);
        Assert.Null(bannedUser.UntilDate);
        Assert.NotNull(bannedUser.User);
        Assert.Equal(12345, bannedUser.User.Id);
        Assert.True(bannedUser.User.IsBot);
        Assert.Equal("First Name", bannedUser.User.FirstName);
        Assert.Equal("Last Name", bannedUser.User.LastName);
        Assert.Equal("test_bot", bannedUser.User.Username);
        Assert.Equal("en_US", bannedUser.User.LanguageCode);
    }

    [Fact]
    public void Should_Return_MessageOriginChat_Info_From_Obsolete_Forward_Properties2()
    {
        // language=JSON
        const string json =
            """
            {
              "update_id": 1234,
              "chat_member": {
                "new_chat_member": {
                    "status": "kicked",
                    "user": {
                        "id": 12345,
                        "is_bot": true,
                        "first_name": "First Name",
                        "last_name": "Last Name",
                        "username": "test_bot",
                        "language_code": "en_US"
                    }
                },
                "old_chat_member": {
                    "status": "left",
                    "user": {
                        "id": 12345,
                        "is_bot": true,
                        "first_name": "First Name",
                        "last_name": "Last Name",
                        "username": "test_bot",
                        "language_code": "en_US"
                    }
                },
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
                "date": 1526315997
              }
            }
            """;

        Update? update = JsonSerializer.Deserialize<Update>(json, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(update);
        Assert.Equal(UpdateType.ChatMember, update.Type);
        Assert.NotNull(update.ChatMember);
        Assert.IsAssignableFrom<ChatMemberBanned>(update.ChatMember.NewChatMember);
        Assert.IsAssignableFrom<ChatMemberLeft>(update.ChatMember.OldChatMember);
    }
}
