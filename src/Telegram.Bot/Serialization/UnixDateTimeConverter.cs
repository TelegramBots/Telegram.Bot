namespace Telegram.Bot.Serialization;

internal sealed class UnixDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => UnixDateTimeConverterUtil.Read(ref reader, typeToConvert);

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        => UnixDateTimeConverterUtil.Write(writer, value);
}
