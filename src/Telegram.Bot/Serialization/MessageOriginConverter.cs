namespace Telegram.Bot.Serialization;

public class MessageOriginConverter : JsonConverter<MessageOrigin?>
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(MessageOrigin);

    public override MessageOrigin? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader, out var element))
        {
            throw new JsonException("Could not read JSON value");
        }

        try
        {
            var root = element.RootElement;
            var type = root.GetProperty("type").GetString() ?? throw new JsonException("Type property not found");

            return type switch
            {
                "user" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.MessageOriginUser),
                "hidden_user" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.MessageOriginHiddenUser),
                "chat" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.MessageOriginChat),
                "channel" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.MessageOriginChannel),
                null => null,
                _ => throw new JsonException($"Unsupported message origin type: {type}")
            };
        }
        finally
        {
            element.Dispose();
        }
    }

    public override void Write(Utf8JsonWriter writer, MessageOrigin? value, JsonSerializerOptions options)
    {
        if (value is not null)
        {
            writer.WriteRawValue(value switch
            {
                MessageOriginUser user =>
                    JsonSerializer.Serialize(user, TelegramBotClientJsonSerializerContext.Instance.MessageOriginUser),
                MessageOriginHiddenUser hiddenUser =>
                    JsonSerializer.Serialize(hiddenUser, TelegramBotClientJsonSerializerContext.Instance.MessageOriginHiddenUser),
                MessageOriginChat chat =>
                    JsonSerializer.Serialize(chat, TelegramBotClientJsonSerializerContext.Instance.MessageOriginChat),
                MessageOriginChannel channel =>
                    JsonSerializer.Serialize(channel, TelegramBotClientJsonSerializerContext.Instance.MessageOriginChannel),
                _ => throw new JsonException("Unsupported message origin type")
            });
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
