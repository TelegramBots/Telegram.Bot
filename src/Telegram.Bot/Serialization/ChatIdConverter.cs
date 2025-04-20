namespace Telegram.Bot.Serialization;

internal sealed class ChatIdConverter : JsonConverter<ChatId?>
{
    public override ChatId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => JsonElement.TryParseValue(ref reader, out var element) ? new(element.Value.ToString()) : null;

    public override void Write(Utf8JsonWriter writer, ChatId? value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case { Username: {} username }: writer.WriteStringValue(username); break;
            case { Identifier: {} identifier }: writer.WriteNumberValue(identifier); break;
            case null: writer.WriteNullValue(); break;
            default: throw new JsonException("Chat ID value is incorrect");
        }
    }
}
