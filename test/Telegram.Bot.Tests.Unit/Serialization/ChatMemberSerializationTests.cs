using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class ChatMemberSerializationTests
{
    [Fact]
    public void Should_Deserialize_Chat_Member_Member()
    {
        var creator = new
        {
            status = ChatMemberStatus.Creator,
            user = new
            {
                id = 12345,
                is_bot = true,
                first_name = "First Name",
                last_name = "Last Name",
                username = "test_bot",
                language_code = "en_US",
            },
            is_anonymous = true,
            custom_title = "custom test title"
        };

        string? chatMemberJson = JsonConvert.SerializeObject(creator, Formatting.Indented);
        ChatMember? chatMember = JsonConvert.DeserializeObject<ChatMember>(chatMemberJson);

        ChatMemberOwner owner = Assert.IsType<ChatMemberOwner>(chatMember);

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

        string chatMemberJson = JsonConvert.SerializeObject(creator);
        JObject j = JObject.Parse(chatMemberJson);

        Assert.Equal(4, j.Children().Count());
        Assert.Equal("creator", j["status"]);
        Assert.Equal(true, j["is_anonymous"]);
        Assert.Equal("Custom test title", j["custom_title"]);

        JToken? ju = j["user"];
        Assert.NotNull(ju);

        Assert.Equal(6, ju.Children().Count());
        Assert.Equal(12345, ju["id"]);
        Assert.Equal(true, ju["is_bot"]);
        Assert.Equal("First Name", ju["first_name"]);
        Assert.Equal("Last Name", ju["first_name"]);
        Assert.Equal("test_bot", ju["username"]);
        Assert.Equal("en_US", ju["language_code"]);
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

        string chatMemberJson = JsonConvert.SerializeObject(creator);
        JObject j = JObject.Parse(chatMemberJson);

        Assert.Equal(3, j.Children().Count());
        Assert.Equal(1617321600, j["until_date"]);
        Assert.Equal("kicked", j["status"]);
        Assert.True(j.ContainsKey("user"));

        JToken? ju = j["user"];
        Assert.NotNull(ju);

        Assert.Equal(6, ju.Children().Count());
        Assert.Equal(12345, ju["id"]);
        Assert.Equal(true, ju["is_bot"]);
        Assert.Equal("First Name", ju["first_name"]);
        Assert.Equal("Last Name", ju["first_name"]);
        Assert.Equal("test_bot", ju["username"]);
        Assert.Equal("en_US", ju["language_code"]);
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

        string chatMemberJson = JsonConvert.SerializeObject(creator);
        JObject j = JObject.Parse(chatMemberJson);

        Assert.Equal(2, j.Children().Count());
        Assert.False(j.ContainsKey("until_date"));
        Assert.Equal("kicked", j["status"]);

        JToken? ju = j["user"];
        Assert.NotNull(ju);

        Assert.Equal(6, ju.Children().Count());
        Assert.Equal(12345, ju["id"]);
        Assert.Equal(true, ju["is_bot"]);
        Assert.Equal("First Name", ju["first_name"]);
        Assert.Equal("Last Name", ju["first_name"]);
        Assert.Equal("test_bot", ju["username"]);
        Assert.Equal("en_US", ju["language_code"]);
    }

    [Fact]
    public void Should_Deserialize_Chat_Member_Banned()
    {
        string json = """
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

        ChatMemberBanned? bannedUser = JsonConvert.DeserializeObject<ChatMemberBanned>(json);

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
        string json = """
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

        ChatMemberBanned? bannedUser = JsonConvert.DeserializeObject<ChatMemberBanned>(json);

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
        string json = """
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

        ChatMemberBanned? bannedUser = JsonConvert.DeserializeObject<ChatMemberBanned>(json);

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
}
