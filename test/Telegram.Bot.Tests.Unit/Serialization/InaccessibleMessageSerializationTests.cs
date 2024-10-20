using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class InaccessibleMessageSerializationTests
{
    [Fact]
    public void Should_Deserialize_InaccessibleMessage_CallbackQuery()
    {
        // language=JSON
        const string json = """
        {
            "id": "12345",
            "from": {
                "id": 1234567,
                "is_bot": false,
                "first_name": "Telegram_Bots",
                "last_name": null,
                "username": "TelegramBots",
                "language_code": null
            },
            "message": {
                "chat": {
                    "id": 9999999999,
                    "first_name": "Telegram_Bots",
                    "last_name": null,
                    "username": "TelegramBots",
                    "type": "private"
                },
                "message_id": 9999,
                "date": 0
            },
            "inline_message_id": "67890",
            "chat_instance": "chat_instance"
        }
        """;

        CallbackQuery? query = JsonSerializer.Deserialize<CallbackQuery>(json, JsonBotAPI.Options);

        Assert.NotNull(query);
        Assert.Equal("12345", query.Id);
        Assert.Equal("67890", query.InlineMessageId);
        Assert.Equal("chat_instance", query.ChatInstance);
        Assert.NotNull(query.From?.Username);

        Assert.NotNull(query.Message);
        Assert.Equal(9999, query.Message.Id);
        Assert.Equal(9999999999L, query.Message.Chat.Id);
        Assert.Equal(default, query.Message.Date);
        Assert.Equal(MessageType.Unknown, query.Message.Type);
        Assert.NotNull(query.Message.Chat?.Username);
    }
}
