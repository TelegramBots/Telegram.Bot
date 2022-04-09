using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Telegram.Bot.Converters.SystemTextJson;

internal class UnixDateTimeConverter : JsonConverter<DateTime?>
{
    private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Null => null,
            JsonTokenType.String => ReadFromString(reader.GetString()),
            JsonTokenType.Number => ReadFromInt64(reader.GetInt64()),
            var tokenType => throw new NotSupportedException(
                $"JSON token {tokenType} is not supported for DateTime deserialization")
        };
    }

    private DateTime ReadFromString(string? timestampText)
    {
        if (!long.TryParse(timestampText, out long timestamp))
            throw new ArgumentException("Input string doesn't seem to contain a valid Int64 number",
                nameof(timestampText));

        return ReadFromInt64(timestamp);
    }

    private DateTime ReadFromInt64(long timestamp)
    {
        if (timestamp < 0L)
            throw new ArgumentException("Timestamp should be greater than or equal to 0", nameof(timestamp));

        return UnixEpoch.AddSeconds(timestamp);
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
        }
        else
        {
            writer.WriteNumberValue((long)(value.Value - UnixEpoch).TotalSeconds);
        }
    }
}
