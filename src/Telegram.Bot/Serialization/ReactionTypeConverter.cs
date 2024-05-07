namespace Telegram.Bot.Serialization;

public class ReactionTypeConverter: JsonConverter<ReactionType?>
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(ReactionType);

    public override ReactionType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                "emoji" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.ReactionTypeEmoji),
                "custom_emoji" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.ReactionTypeCustomEmoji),
                null => null,
                _ => throw new JsonException($"Unsupported reaction type: {type}")
            };
        }
        finally
        {
            element.Dispose();
        }
    }
    public override void Write(Utf8JsonWriter writer, ReactionType? value, JsonSerializerOptions options)
    {
        if (value is not null)
        {
            writer.WriteRawValue(value switch
            {
                ReactionTypeEmoji emoji =>
                    JsonSerializer.Serialize(emoji, TelegramBotClientJsonSerializerContext.Instance.ReactionTypeEmoji),
                ReactionTypeCustomEmoji customEmoji =>
                    JsonSerializer.Serialize(customEmoji, TelegramBotClientJsonSerializerContext.Instance.ReactionTypeCustomEmoji),
                _ => throw new JsonException("Unsupported reaction type")
            });
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
