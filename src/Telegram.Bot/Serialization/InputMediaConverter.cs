namespace Telegram.Bot.Serialization;

public class InputMediaConverter : JsonConverter<InputMedia?>
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(InputMedia);

    public override InputMedia? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                "photo" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.InputMediaPhoto),
                "video" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.InputMediaVideo),
                "animation" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.InputMediaAnimation),
                "audio" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.InputMediaAudio),
                "document" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.InputMediaDocument),
                null => null,
                _ => throw new JsonException($"Unsupported input media: {type}")
            };
        }
        finally
        {
            element.Dispose();
        }
    }

    public override void Write(Utf8JsonWriter writer, InputMedia? value, JsonSerializerOptions options)
    {
        if (value is not null)
        {
            writer.WriteRawValue(value switch
            {
                InputMediaPhoto photo => JsonSerializer.Serialize(photo, TelegramBotClientJsonSerializerContext.Instance.InputMediaPhoto),
                InputMediaVideo video => JsonSerializer.Serialize(video, TelegramBotClientJsonSerializerContext.Instance.InputMediaVideo),
                InputMediaAnimation animation => JsonSerializer.Serialize(animation, TelegramBotClientJsonSerializerContext.Instance.InputMediaAnimation),
                InputMediaAudio audio => JsonSerializer.Serialize(audio, TelegramBotClientJsonSerializerContext.Instance.InputMediaAudio),
                InputMediaDocument document => JsonSerializer.Serialize(document, TelegramBotClientJsonSerializerContext.Instance.InputMediaDocument),
                _ => throw new JsonException("Unsupported input media")
            });
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}