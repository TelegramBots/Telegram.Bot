using System.Text.Json;

namespace Telegram.Bot.Converters.SystemTextJson;

internal class ChatIdConverter : System.Text.Json.Serialization.JsonConverter<ChatId?>
{
    public override ChatId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonElement.ParseValue(ref reader).GetString() is { } value ? new ChatId(value) : null;
    }

    public override void Write(Utf8JsonWriter writer, ChatId? value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case { Username: {} username }:
                writer.WriteStringValue(username);
                break;
            case { Identifier: {} identifier }:
                writer.WriteNumberValue(identifier);
                break;
            case null:
                writer.WriteNullValue();
                break;
            default:
                throw new JsonSerializationException();
        }
    }
}
