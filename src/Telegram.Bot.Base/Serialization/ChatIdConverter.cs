namespace Telegram.Bot.Serialization;

/// <summary>
/// Converts <see cref="ChatId"/> to and from JSON
/// </summary>
public class ChatIdConverter : JsonConverter<ChatId?>
{
    /// <summary>
    /// Determines whether the specified type can be converted.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override ChatId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonElement.TryParseValue(ref reader, out var element))
            return null;

        return new(element.Value.ToString());
    }

    /// <summary>
    /// Writes the JSON representation of the object.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    /// <exception cref="JsonException"></exception>
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
