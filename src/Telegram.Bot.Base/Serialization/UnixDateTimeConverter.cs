namespace Telegram.Bot.Serialization;

/// <summary>
/// Converts <see cref="DateTime"/> to Unix timestamp and vice versa
/// </summary>
public class UnixDateTimeConverter : JsonConverter<DateTime>
{
    /// <summary>
    /// Reads a Unix timestamp from the reader and converts it to a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => UnixDateTimeConverterUtil.Read(ref reader, typeToConvert, options);

    /// <summary>
    /// Writes a <see cref="DateTime"/> value as a Unix timestamp.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        => UnixDateTimeConverterUtil.Write(writer, value, options);
}
