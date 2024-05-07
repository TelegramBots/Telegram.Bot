namespace Telegram.Bot.Serialization;

/// <summary>
/// Converts <see cref="DateTime"/> to its Unix timestamp representation and vice versa
/// </summary>
public class BanTimeConverter : JsonConverter<DateTime?>
{
    /// <summary>
    /// Determines whether the specified type can be converted.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="JsonException"></exception>
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var underlyingType = Nullable.GetUnderlyingType(typeToConvert);
        if (reader.TokenType is JsonTokenType.Null)
        {
            if (underlyingType is null)
                throw new JsonException($"Cannot convert null value to {typeToConvert:CultureInfo.InvariantCulture}.");

            return default;
        }

        var clonedReader = reader;
        var value = clonedReader.GetInt64();

        return value is 0L
            ? null
            : UnixDateTimeConverterUtil.Read(ref reader, typeToConvert, options);
    }

    /// <summary>
    /// Writes the JSON representation of the object.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value is null || value.Value == default)
            writer.WriteNumberValue(0);
        else
            UnixDateTimeConverterUtil.Write(writer, value.Value, options);
    }
}
