using System;
using System.Text.Json;

namespace Telegram.Bot.Converters.SystemTextJson;

internal class BanTimeUnixDateTimeConverter : UnixDateTimeConverter
{
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        bool nonNullable = Nullable.GetUnderlyingType(typeToConvert) is null;

        return reader.TokenType == JsonTokenType.Number && reader.GetInt64() is 0L
            ? nonNullable
                ? default(DateTime)
                : null
            : base.Read(ref reader, typeToConvert, options);
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value is null || value.Equals(default(DateTime)))
        {
            writer.WriteNumberValue(0);
        }
        else
        {
            base.Write(writer, value, options);
        }
    }
}
