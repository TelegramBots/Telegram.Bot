namespace Telegram.Bot.Serialization;

internal sealed class RichTextConverter : PolymorphicJsonConverter<RichText>
{
    public override RichText Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.StartArray => new RichTextArray { Array = JsonSerializer.Deserialize<RichText[]>(ref reader, options)! },
            JsonTokenType.String => new RichTextText { Text = reader.GetString()! },
            JsonTokenType.StartObject => base.Read(ref reader, objectType, options),
            _ => throw new JsonException($"Unexpected token {reader.TokenType} when trying to deserialize RichText"),
        };
    }

    public override void Write(Utf8JsonWriter writer, RichText? value, JsonSerializerOptions options)
    {
        switch (value?.Type)
        {
            case RichTextType.Array:
                JsonSerializer.Serialize(writer, ((RichTextArray)value).Array, options);
                break;
            case RichTextType.Text:
                writer.WriteStringValue(((RichTextText)value).Text);
                break;
            default:
                base.Write(writer, value, options);
                break;
        }
    }
}
