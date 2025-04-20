namespace Telegram.Bot.Serialization;

internal sealed class BanTimeConverter : JsonConverter<DateTime?>
{
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var underlyingType = Nullable.GetUnderlyingType(typeToConvert);
        if (reader.TokenType is JsonTokenType.Null)
            return underlyingType is not null ? null : throw new JsonException($"Cannot convert null value to {typeToConvert}.");
        var clonedReader = reader;
        var value = clonedReader.GetInt64();
        return value is 0L ? null : UnixDateTimeConverterUtil.Read(ref reader, typeToConvert);
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value is null || value.Value == default)
            writer.WriteNumberValue(0);
        else
            UnixDateTimeConverterUtil.Write(writer, value.Value);
    }
}
