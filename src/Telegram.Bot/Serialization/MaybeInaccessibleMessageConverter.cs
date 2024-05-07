namespace Telegram.Bot.Serialization;

public class MaybeInaccessibleMessageConverter: JsonConverter<MaybeInaccessibleMessage?>
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(MaybeInaccessibleMessage);

    public override MaybeInaccessibleMessage? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader, out var element))
        {
            throw new JsonException("Could not read JSON value");
        }

        try
        {
            var root = element.RootElement;
            var date = root.GetProperty("date").GetInt64();

            return date switch
            {
                0 => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.InaccessibleMessage),
                _ => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.Message),
            };
        }
        finally
        {
            element.Dispose();
        }
    }

    public override void Write(Utf8JsonWriter writer, MaybeInaccessibleMessage? value, JsonSerializerOptions options)
    {
        if (value is not null)
        {
            writer.WriteRawValue(value switch
            {
                Message message =>
                    JsonSerializer.Serialize(message, TelegramBotClientJsonSerializerContext.Instance.Message),
                InaccessibleMessage inaccessibleMessage =>
                    JsonSerializer.Serialize(inaccessibleMessage, TelegramBotClientJsonSerializerContext.Instance.InaccessibleMessage),
                _ => throw new JsonException("Unsupported message type")
            });
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
