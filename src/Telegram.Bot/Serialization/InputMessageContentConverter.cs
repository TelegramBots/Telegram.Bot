using Telegram.Bot.Types.InlineQueryResults;

namespace Telegram.Bot.Serialization;

public class InputMessageContentConverter : JsonConverter<InputMessageContent?>
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(InputMessageContent);

    // ReSharper disable once CognitiveComplexity
    public override InputMessageContent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader, out var element))
        {
            throw new JsonException("Could not read JSON value");
        }

        try
        {
            var root = element.RootElement;

            if (root.TryGetProperty("message_text", out  _))
            {
                return JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.InputTextMessageContent);
            }

            if (root.TryGetProperty("phone_number", out  _) && root.TryGetProperty("first_name", out  _))
            {
                return JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.InputContactMessageContent);
            }

            if (root.TryGetProperty("latitude", out  _) && root.TryGetProperty("longitude", out  _))
            {
                if (root.TryGetProperty("title", out _) && root.TryGetProperty("address", out _))
                {
                    return JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.InputVenueMessageContent);
                }

                return JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.InputLocationMessageContent);
            }

            if (root.TryGetProperty("payload", out _) && root.TryGetProperty("provider_token", out _) && root.TryGetProperty("currency", out _))
            {
                return JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.InputInvoiceMessageContent);
            }

            throw new JsonException($"Unsupported message content type");
        }
        finally
        {
            element.Dispose();
        }
    }

    public override void Write(Utf8JsonWriter writer, InputMessageContent? value, JsonSerializerOptions options)
    {
        if (value is not null)
        {
            writer.WriteRawValue(value switch
            {
                InputTextMessageContent textMessageContent =>
                    JsonSerializer.Serialize(textMessageContent, TelegramBotClientJsonSerializerContext.Instance.InputTextMessageContent),
                InputContactMessageContent contactMessageContent =>
                    JsonSerializer.Serialize(contactMessageContent, TelegramBotClientJsonSerializerContext.Instance.InputContactMessageContent),
                InputLocationMessageContent locationMessageContent =>
                    JsonSerializer.Serialize(locationMessageContent, TelegramBotClientJsonSerializerContext.Instance.InputLocationMessageContent),
                InputVenueMessageContent venueMessageContent =>
                    JsonSerializer.Serialize(venueMessageContent, TelegramBotClientJsonSerializerContext.Instance.InputVenueMessageContent),
                InputInvoiceMessageContent invoiceMessageContent =>
                    JsonSerializer.Serialize(invoiceMessageContent, TelegramBotClientJsonSerializerContext.Instance.InputInvoiceMessageContent),
                _ => throw new JsonException("Unsupported message content type")
            });
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
