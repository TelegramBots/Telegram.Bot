namespace Telegram.Bot.Serialization;

/// <summary>
/// Converts <see cref="Color"/> to its integer representation and vice versa
/// </summary>
public class ColorConverter : JsonConverter<Color>
{
    /// <summary>
    /// Determines whether the specified type can be converted.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => JsonElement.TryParseValue(ref reader, out var element)
           && element.Value.TryGetUInt32(out var intValue)
            ? new Color(intValue)
            : new();

    /// <summary>
    /// Writes the JSON representation of the object.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        => writer.WriteNumberValue(value.ToInt());
}
