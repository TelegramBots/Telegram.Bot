using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class ChatSourceBoostSerializationTests
{
    [Fact]
    public void Should_Serialize_ChatBoostSourcePremium()
    {
        ChatBoostSourcePremium creator = new()
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
        Assert.Equal("premium", j["source"]);

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
    public void Should_Serialize_ChatBoostSourceGiveaway()
    {
        ChatBoostSourceGiveaway creator = new()
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
            GiveawayMessageId = 654321,
            IsUnclaimed = true,
        };

        string chatMemberJson = JsonConvert.SerializeObject(creator);
        JObject j = JObject.Parse(chatMemberJson);

        Assert.Equal(4, j.Children().Count());
        Assert.Equal("giveaway", j["source"]);
        Assert.Equal(654321, j["giveaway_message_id"]);
        Assert.Equal(true, j["is_unclaimed"]);

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
    public void Should_Serialize_ChatBoostSourceGiftCode()
    {
        ChatBoostSourceGiftCode creator = new()
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
    public void Should_Deserialize_ChatBoostPremium()
    {
        const string json = """
        {
            "source": "premium",
            "user": {
                "id": 123,
                "is_premium": true,
                "is_bot": false,
                "first_name": "Test User"
            }
        }
        """;

        ChatBoostSource? boostSource = JsonConvert.DeserializeObject<ChatBoostSource>(json);

        ChatBoostSourcePremium premium = Assert.IsAssignableFrom<ChatBoostSourcePremium>(boostSource);

        Assert.Equal(ChatBoostSourceType.Premium, premium.Source);
        Assert.Equal(123, premium.User.Id);
        Assert.True(premium.User.IsPremium);
        Assert.NotNull(premium.User);
        Assert.Equal("Test User", premium.User.FirstName);
    }


    [Fact]
    public void Should_Deserialize_ChatBoostSourceGiftCode()
    {
        string json = """
        {
            "source": "gift_code",
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

        ChatBoostSourceGiftCode? giveaway = JsonConvert.DeserializeObject<ChatBoostSourceGiftCode>(json);

        Assert.NotNull(giveaway);
        Assert.Equal(ChatBoostSourceType.GiftCode, giveaway.Source);
        Assert.NotNull(giveaway.User);
        Assert.Equal(12345, giveaway.User.Id);
        Assert.True(giveaway.User.IsBot);
        Assert.Equal("First Name", giveaway.User.FirstName);
        Assert.Equal("Last Name", giveaway.User.LastName);
        Assert.Equal("test_bot", giveaway.User.Username);
        Assert.Equal("en_US", giveaway.User.LanguageCode);
    }

    [Fact]
    public void Should_Deserialize_ChatBoostSourceGiveaway()
    {
        string json = """
        {
            "source": "giveaway",
            "giveaway_message_id": 12345,
            "is_unclaimed": true,
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

        ChatBoostSourceGiveaway? giveaway = JsonConvert.DeserializeObject<ChatBoostSourceGiveaway>(json);

        Assert.NotNull(giveaway);
        Assert.Equal(ChatBoostSourceType.Giveaway, giveaway.Source);
        Assert.True(giveaway.IsUnclaimed);
        Assert.Equal(12345, giveaway.GiveawayMessageId);
        Assert.NotNull(giveaway.User);
        Assert.Equal(12345, giveaway.User.Id);
        Assert.True(giveaway.User.IsBot);
        Assert.Equal("First Name", giveaway.User.FirstName);
        Assert.Equal("Last Name", giveaway.User.LastName);
        Assert.Equal("test_bot", giveaway.User.Username);
        Assert.Equal("en_US", giveaway.User.LanguageCode);
    }
}
