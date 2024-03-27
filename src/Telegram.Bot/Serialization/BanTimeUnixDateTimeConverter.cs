namespace Telegram.Bot.Serialization;

internal class BanTimeConverter : JsonConverter<DateTime?>
{
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

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value is null || value.Value == default)
            writer.WriteNumberValue(0);
        else
            UnixDateTimeConverterUtil.Write(writer, value.Value, options);
    }
}
