using System.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;


namespace Telegram.Bot.Tests.Unit.Serialization;

public class PhotoMessageSerializationTests
{
    [Fact(DisplayName = "Should deserialize a photo message")]
    public void Should_Deserialize_PhotoMessage()
    {
        // language=JSON
        const string json = """
        {
            "message_id": 1234,
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
            "photo": [
                {
                    "file_id": "AgADAgADvKgxGxW80EtRgjrTaWNmy7UerQ4ABN7x5HqnrHW_wp4BAAEC",
                    "file_unique_id": "AgADcOsAAhUdZAc",
                    "file_size": 3134,
                    "width": 90,
                    "height": 90
                },
                {
                    "file_id": "AgADAgADvKgxGxW80EtRgjrTaWNmy7UerQ4ABIrxzSBLXOQYw54BAAEC",
                    "file_unique_id": "AgADcOsAAhUdZAc",
                    "file_size": 52433,
                    "width": 320,
                    "height": 320
                },
                {
                    "file_id": "AgADAgADvKgxGxW80EtRgjrTaWNmy7UerQ4ABIJONRZpTJFnxJ4BAAEC",
                    "file_unique_id": "AgADcOsAAhUdZAc",
                    "file_size": 231019,
                    "width": 800,
                    "height": 800
                },
                {
                    "file_id": "AgADAgADvKgxGxW80EtRgjrTaWNmy7UerQ4ABP6uRLtwe8Z8wZ4BAAEC",
                    "file_unique_id": "AgADcOsAAhUdZAc",
                    "file_size": 489108,
                    "width": 1280,
                    "height": 1280
                }
            ]
        }
        """;

        Message? message = JsonSerializer.Deserialize<Message>(json, JsonBotAPI.Options);

        Assert.NotNull(message);
        Assert.Equal(MessageType.Photo, message.Type);
        Assert.NotNull(message.Photo);
        Assert.NotEmpty(message.Photo);
        Assert.All(message.Photo.Select(ps => ps.FileId), Assert.NotEmpty);
        Assert.All(message.Photo.Select(ps => ps.Width), w => Assert.NotEqual(0, w));
        Assert.All(message.Photo.Select(ps => ps.Height), h => Assert.NotEqual(0, h));
    }

    [Fact(DisplayName = "Should serialize a photo message")]
    public void Should_Serialize_PhotoMessage()
    {
        Message message = new()
        {
            Id = 1234,
            From = new()
            {
                Id = 1234567,
                FirstName = "Telegram_Bots",
                Username = "TelegramBots",
            },
            Chat = new()
            {
                Id = 1234567,
                FirstName = "Telegram_Bots",
                Username = "TelegramBots",
                Type = ChatType.Private
            },
            Date = new(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            Photo =
            [
                new()
                {
                    FileId = "AgADAgADvKgxGxW80EtRgjrTaWNmy7UerQ4ABN7x5HqnrHW_wp4BAAEC",
                    FileUniqueId = "AgADcOsAAhUdZAc",
                    FileSize = 3134,
                    Width = 90,
                    Height = 90,
                },
                new()
                {
                    FileId = "AgADAgADvKgxGxW80EtRgjrTaWNmy7UerQ4ABIrxzSBLXOQYw54BAAEC",
                    FileUniqueId = "AgADcOsAAhUdZAc",
                    FileSize = 52433,
                    Width = 320,
                    Height = 320,
                },
                new()
                {
                    FileId = "AgADAgADvKgxGxW80EtRgjrTaWNmy7UerQ4ABIJONRZpTJFnxJ4BAAEC",
                    FileUniqueId = "AgADcOsAAhUdZAc",
                    FileSize = 231019,
                    Width = 800,
                    Height = 800,
                },
                new()
                {
                    FileId = "AgADAgADvKgxGxW80EtRgjrTaWNmy7UerQ4ABP6uRLtwe8Z8wZ4BAAEC",
                    FileUniqueId = "AgADcOsAAhUdZAc",
                    FileSize = 489108,
                    Width = 1280,
                    Height = 1280,
                }
            ]
        };

        string json = JsonSerializer.Serialize(message, JsonBotAPI.Options);
        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(5, j.Count);
        Assert.Equal(1234, (long?)j["message_id"]);
        Assert.Equal(1514764800, (long?)j["date"]);

        JsonNode? chatNode = j["chat"];
        Assert.NotNull(chatNode);
        JsonObject jChat = Assert.IsAssignableFrom<JsonObject>(chatNode);
        Assert.NotNull(jChat);
        Assert.Equal(4, jChat.Count);
        Assert.Equal(1234567, (long?)jChat["id"]);
        Assert.Equal("Telegram_Bots", (string?)jChat["first_name"]);
        Assert.Equal("TelegramBots", (string?)jChat["username"]);
        Assert.Equal("private", (string?)jChat["type"]);

        JsonNode? fromNode = j["from"];
        Assert.NotNull(fromNode);
        JsonObject jFrom = Assert.IsAssignableFrom<JsonObject>(fromNode);
        Assert.NotNull(jFrom);
        Assert.Equal(3, jFrom.Count);
        Assert.Equal(1234567, (long?)jFrom["id"]);
        Assert.Equal("Telegram_Bots", (string?)jFrom["first_name"]);
        Assert.Equal("TelegramBots", (string?)jFrom["username"]);
        Assert.Null((bool?)jFrom["is_bot"]);

        JsonNode? photoNode = j["photo"];
        Assert.NotNull(photoNode);
        JsonArray jPhoto = Assert.IsAssignableFrom<JsonArray>(photoNode);
        Assert.NotNull(jPhoto);
        Assert.Equal(4, jPhoto.Count);
        Assert.All(jPhoto, photo =>
        {
            JsonObject p = Assert.IsAssignableFrom<JsonObject>(photo);
            Assert.Equal(5, p.Count);
            Assert.NotNull(p["file_id"]);
            Assert.Equal(JsonValueKind.String, p["file_id"]!.GetValueKind());

            Assert.NotNull(p["file_unique_id"]);
            Assert.Equal(JsonValueKind.String, p["file_unique_id"]!.GetValueKind());

            Assert.NotNull(p["file_size"]);
            Assert.Equal(JsonValueKind.Number, p["file_size"]!.GetValueKind());

            Assert.NotNull(p["width"]);
            Assert.Equal(JsonValueKind.Number, p["width"]!.GetValueKind());

            Assert.NotNull(p["height"]);
            Assert.Equal(JsonValueKind.Number, p["height"]!.GetValueKind());
        });
    }
}
