namespace Telegram.Bot.Serialization;

/// <summary>
/// Utility class for <see cref="UnixDateTimeConverter"/>.
/// </summary>
public static class UnixDateTimeConverterUtil
{
    private static readonly DateTime UnixEpoch = new(
        year: 1970,
        month: 1,
        day: 1,
        hour: 0,
        minute: 0,
        second: 0,
        kind: DateTimeKind.Utc
    );

    /// <summary>
    /// Reads a Unix timestamp from the reader and converts it to a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="JsonException"></exception>
    public static DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var seconds = reader.GetInt64();
        if (seconds < 0)
            throw new JsonException($"Cannot convert value that is before Unix epoch of 00:00:00 UTC on 1 January 1970 to {typeToConvert:CultureInfo.InvariantCulture}.");

        return UnixEpoch.AddSeconds(seconds);
    }

    /// <summary>
    /// Writes a <see cref="DateTime"/> value as a Unix timestamp.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    /// <exception cref="JsonException"></exception>
    public static void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        long seconds = (long)(value.ToUniversalTime() - UnixEpoch).TotalSeconds;
        if (seconds < 0)
            throw new JsonException("Cannot convert date value that is before Unix epoch of 00:00:00 UTC on 1 January 1970.");

        writer.WriteNumberValue(seconds);
    }
}
