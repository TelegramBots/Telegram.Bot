namespace Telegram.Bot.Serialization;

public class ChatBoostSourceConverter : JsonConverter<ChatBoostSource?>
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(ChatBoostSource);

    public override ChatBoostSource? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader, out var element))
        {
            throw new JsonException("Could not read JSON value");
        }

        try
        {
            var root = element.RootElement;
            var source = root.GetProperty("source").GetString() ?? throw new JsonException("Source property not found");

            return source switch
            {
                "premium" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.ChatBoostSourcePremium),
                "gift_code" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.ChatBoostSourceGiftCode),
                "giveaway" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.ChatBoostSourceGiveaway),
                null => null,
                _ => throw new JsonException($"Unsupported chat boost source: {source}")
            };
        }
        finally
        {
            element.Dispose();
        }
    }

    public override void Write(Utf8JsonWriter writer, ChatBoostSource? value, JsonSerializerOptions options)
    {
        if (value is not null)
        {
            writer.WriteRawValue(value switch
            {
                ChatBoostSourcePremium premium => JsonSerializer.Serialize(premium, TelegramBotClientJsonSerializerContext.Instance.ChatBoostSourcePremium),
                ChatBoostSourceGiftCode giftCode => JsonSerializer.Serialize(giftCode, TelegramBotClientJsonSerializerContext.Instance.ChatBoostSourceGiftCode),
                ChatBoostSourceGiveaway giveaway => JsonSerializer.Serialize(giveaway, TelegramBotClientJsonSerializerContext.Instance.ChatBoostSourceGiveaway),
                _ => throw new JsonException("Unsupported boost source type")
            });
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
