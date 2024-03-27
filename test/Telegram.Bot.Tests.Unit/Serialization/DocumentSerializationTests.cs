using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class DocumentSerializationTests
{
    [Fact(DisplayName = "Should serialize a document message")]
    public void Should_Serialize_DocumentMessage()
    {
        Message documentMessage = new()
        {
            MessageId = 1234,
            From = new()
            {
                Id = 123_456_789,
                FirstName = "TelegramBots",
                Username = "Telegram_Bots"
            },
            Chat = new()
            {
                Id = -9_877_654_320_000,
                Title = "Test Chat",
                Type = ChatType.Supergroup,
                CanSetStickerSet = true
            },
            Document = new()
            {
                FileId = "KLAHCVUydfS_jHIBildtwpmvxZg",
                FileUniqueId = "AgADcOsAAhUdZAc",
                FileName = "test_file.txt",
                FileSize = 123_456,
                MimeType = "plain/text"
            },
            Date = new DateTime(
                year: 2024,
                month: 1,
                day: 1,
                hour: 10,
                minute: 0,
                second: 0,
                kind: DateTimeKind.Utc
            ),
            Caption = "Test Document Description"
        };

        string json = JsonSerializer.Serialize(documentMessage, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(json);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(6, j.Count);
        Assert.Equal(1234, (long?)j["message_id"]);
        Assert.Equal("Test Document Description", (string?)j["caption"]);
        Assert.Equal(1704103200, (long?)j["date"]);

        JsonNode? documentNode = j["document"];
        Assert.NotNull(documentNode);
        JsonObject document = Assert.IsAssignableFrom<JsonObject>(documentNode);

        Assert.Equal(5, document.Count);
        Assert.Equal("KLAHCVUydfS_jHIBildtwpmvxZg", (string?)document["file_id"]);
        Assert.Equal("AgADcOsAAhUdZAc", (string?)document["file_unique_id"]);
        Assert.Equal("test_file.txt", (string?)document["file_name"]);
        Assert.Equal(123_456, (int?)document["file_size"]);
        Assert.Equal("plain/text", (string?)document["mime_type"]);

        JsonNode? fromNode = j["from"];
        Assert.NotNull(fromNode);
        JsonObject user = Assert.IsAssignableFrom<JsonObject>(fromNode);
        Assert.Equal(4, user.Count);
        Assert.Equal(123_456_789, (long?)user["id"]);
        Assert.Equal("TelegramBots", (string?)user["first_name"]);
        Assert.Equal("Telegram_Bots", (string?)user["username"]);
        Assert.Equal(false, (bool?)user["is_bot"]);

        JsonNode? chatNode = j["chat"];
        Assert.NotNull(chatNode);
        JsonObject chat = Assert.IsAssignableFrom<JsonObject>(chatNode);
        Assert.Equal(4, chat.Count);
        Assert.Equal(-9_877_654_320_000, (long?)chat["id"]);
        Assert.Equal("Test Chat", (string?)chat["title"]);
        Assert.Equal("supergroup", (string?)chat["type"]);
        Assert.Equal(true, (bool?)chat["can_set_sticker_set"]);
    }

    [Fact(DisplayName = "Should deserialize a document message")]
    public void Should_Deserialize_DocumentMessage()
    {
        // language=JSON
        const string json = """
        {
            "message_id": 1234,
            "from": {
                "id": 123456789,
                "is_bot": false,
                "first_name": "TelegramBots",
                "last_name": null,
                "username": "Telegram_Bots",
                "language_code": null
            },
            "date": 1503172015,
            "chat": {
                "id": -9877654320000,
                "type": "supergroup",
                "title": "Test Chat",
                "username": null,
                "first_name": null,
                "last_name": null,
                "all_members_are_administrators": false,
                "photo": null,
                "description": null,
                "invite_link": null,
                "sticker_set_name": null,
                "can_set_sticker_set": null
            },
            "forward_from": null,
            "forward_from_chat": null,
            "forward_from_message_id": 0,
            "forward_date": null,
            "reply_to_message": null,
            "edit_date": null,
            "text": null,
            "entities": [],
            "caption_entities": [],
            "audio": null,
            "document": {
                "file_name": "test_file.txt",
                "mime_type": "plain/text",
                "file_id": "KLAHCVUydfS_jHIBildtwpmvxZg",
                "file_unique_id": "AgADcOsAAhUdZAc",
                "file_size": 123456,
                "file_path": null
            },
            "game": null,
            "photo": null,
            "sticker": null,
            "video": null,
            "voice": null,
            "video_note": null,
            "caption": "Test Document Description",
            "contact": null,
            "location": null,
            "venue": null,
            "new_chat_member": null,
            "new_chat_members": null,
            "left_chat_member": null,
            "new_chat_title": null,
            "new_chat_photo": null,
            "delete_chat_photo": false,
            "group_chat_created": false,
            "supergroup_chat_created": false,
            "channel_chat_created": false,
            "migrate_to_chat_id": 0,
            "migrate_from_chat_id": 0,
            "pinned_message": null,
            "invoice": null,
            "successful_payment": null
        }
        """;

        Message? message = JsonSerializer.Deserialize<Message>(json, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(message);
        Assert.Equal(MessageType.Document, message.Type);
        Assert.Equal("test_file.txt", message.Document?.FileName);
        Assert.Null(message.Chat.CanSetStickerSet);
    }
}
