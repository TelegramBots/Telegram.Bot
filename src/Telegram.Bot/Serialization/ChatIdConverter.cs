namespace Telegram.Bot.Serialization;

internal class ChatIdConverter : JsonConverter<ChatId?>
{
    public override ChatId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonElement.TryParseValue(ref reader, out var element))
            return null;

        return new(element.Value.ToString());
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
                throw new JsonException("Chat ID value is incorrect");
        }
    }
}
