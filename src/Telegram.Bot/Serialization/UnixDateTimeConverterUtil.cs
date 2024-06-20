namespace Telegram.Bot.Serialization;

internal static class UnixDateTimeConverterUtil
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

    internal static DateTime Read(ref Utf8JsonReader reader, Type typeToConvert)
    {
        var seconds = reader.GetInt64();
        if (seconds > 0)
            return UnixEpoch.AddSeconds(seconds);
        if (seconds == 0)
            return default; // easier to test than 1/1/1970
        throw new JsonException($"Cannot convert value that is before Unix epoch of 00:00:00 UTC on 1 January 1970 to {typeToConvert:CultureInfo.InvariantCulture}.");
    }

    internal static void Write(Utf8JsonWriter writer, DateTime value)
    {
        if (value == default)
            writer.WriteNumberValue(0L);
        else
        {
            long seconds = (long)(value.ToUniversalTime() - UnixEpoch).TotalSeconds;
            if (seconds >= 0)
                writer.WriteNumberValue(seconds);
            else
                throw new JsonException("Cannot convert date value that is before Unix epoch of 00:00:00 UTC on 1 January 1970.");
        }
    }
}
