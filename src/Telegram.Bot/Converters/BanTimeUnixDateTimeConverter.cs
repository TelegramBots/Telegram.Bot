using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Converters
{
    internal class BanTimeUnixDateTimeConverter : UnixDateTimeConverter
    {
        static readonly DateTime DefaultUtc = DateTime.SpecifyKind(value: default, kind: DateTimeKind.Utc);

        public override object? ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var nonNullable = Nullable.GetUnderlyingType(objectType) is null;

            if (reader.TokenType == JsonToken.Integer && reader.Value is 0L)
            {
                return nonNullable ? DefaultUtc : null;
            }

            return base.ReadJson(reader, objectType, existingValue, serializer);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is null || value.Equals(DefaultUtc))
            {
                writer.WriteValue(0);
            }
            else
            {
                base.WriteJson(writer, value, serializer);
            }
        }
    }
}
