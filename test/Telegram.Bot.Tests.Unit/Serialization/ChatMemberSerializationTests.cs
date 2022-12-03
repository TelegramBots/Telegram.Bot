using System;
using Newtonsoft.Json;
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

        string? chatMemberJson = JsonConvert.SerializeObject(creator);
        Assert.Contains(@"""status"":""creator""", chatMemberJson);
        Assert.Contains(@"""is_anonymous"":true", chatMemberJson);
        Assert.Contains(@"""custom_title"":""Custom test title""", chatMemberJson);
        Assert.Contains(@"""user"":{", chatMemberJson);
        Assert.Contains(@"""id"":12345", chatMemberJson);
        Assert.Contains(@"""is_bot"":true", chatMemberJson);
        Assert.Contains(@"""first_name"":""First Name""", chatMemberJson);
        Assert.Contains(@"""last_name"":""Last Name""", chatMemberJson);
        Assert.Contains(@"""username"":""test_bot""", chatMemberJson);
        Assert.Contains(@"""language_code"":""en_US""", chatMemberJson);
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

        string? chatMemberJson = JsonConvert.SerializeObject(creator);

        Assert.Contains(@"""until_date"":1617321600", chatMemberJson);
        Assert.Contains(@"""status"":""kicked""", chatMemberJson);
        Assert.Contains(@"""user"":{", chatMemberJson);
        Assert.Contains(@"""id"":12345", chatMemberJson);
        Assert.Contains(@"""is_bot"":true", chatMemberJson);
        Assert.Contains(@"""first_name"":""First Name""", chatMemberJson);
        Assert.Contains(@"""last_name"":""Last Name""", chatMemberJson);
        Assert.Contains(@"""username"":""test_bot""", chatMemberJson);
        Assert.Contains(@"""language_code"":""en_US""", chatMemberJson);
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

        Assert.DoesNotContain(@"""until_date""", chatMemberJson);
        Assert.Contains(@"""status"":""kicked""", chatMemberJson);
        Assert.Contains(@"""user"":{", chatMemberJson);
        Assert.Contains(@"""id"":12345", chatMemberJson);
        Assert.Contains(@"""is_bot"":true", chatMemberJson);
        Assert.Contains(@"""first_name"":""First Name""", chatMemberJson);
        Assert.Contains(@"""last_name"":""Last Name""", chatMemberJson);
        Assert.Contains(@"""username"":""test_bot""", chatMemberJson);
        Assert.Contains(@"""language_code"":""en_US""", chatMemberJson);
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
