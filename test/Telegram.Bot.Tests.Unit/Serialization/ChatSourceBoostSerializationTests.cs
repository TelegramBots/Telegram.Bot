using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class ChatSourceBoostSerializationTests
{
    [Fact]
    public void Should_Serialize_ChatBoostSourcePremium()
    {
        ChatBoostSource cbs = new ChatBoostSourcePremium()
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

        string json = JsonSerializer.Serialize(cbs, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(2, j.Count);
        Assert.Equal("premium", (string?)j["source"]);

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
    public void Should_Serialize_ChatBoostSourceGiveaway()
    {
        ChatBoostSource cbs = new ChatBoostSourceGiveaway()
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

        string json = JsonSerializer.Serialize(cbs, JsonBotAPI.Options);
        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(4, j.Count);
        Assert.Equal("giveaway", (string?)j["source"]);
        Assert.Equal(654321, (long?)j["giveaway_message_id"]);
        Assert.Equal(true, (bool?)j["is_unclaimed"]);

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
    public void Should_Serialize_ChatBoostSourceGiftCode()
    {
        ChatBoostSource cbs = new ChatBoostSourceGiftCode()
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

        string json = JsonSerializer.Serialize(cbs, JsonBotAPI.Options);
        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(2, j.Count);

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
    public void Should_Deserialize_ChatBoostPremium()
    {
        // language=JSON
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

        ChatBoostSource? boostSource = JsonSerializer.Deserialize<ChatBoostSource>(json, JsonBotAPI.Options);

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
        // language=JSON
        const string json = """
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

        ChatBoostSourceGiftCode? giveaway = JsonSerializer.Deserialize<ChatBoostSourceGiftCode>(json, JsonBotAPI.Options);

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
        // language=JSON
        const string json = """
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

        ChatBoostSourceGiveaway? giveaway = JsonSerializer.Deserialize<ChatBoostSourceGiveaway>(json, JsonBotAPI.Options);

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
