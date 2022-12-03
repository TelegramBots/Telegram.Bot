using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Converters;

internal class BanTimeUnixDateTimeConverter : UnixDateTimeConverter
{
    public override object? ReadJson(
        JsonReader reader,
         Type objectType,
         object? existingValue,
         JsonSerializer serializer)
    {
        var nonNullable = Nullable.GetUnderlyingType(objectType) is null;

        return reader.TokenType is JsonToken.Integer && reader.Value is 0L
            ? nonNullable
                ? default
                : null
            : base.ReadJson(reader, objectType, existingValue, serializer);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is null || value.Equals(default(DateTime)))
        {
            writer.WriteValue(0);
        }
        else
        {
            base.WriteJson(writer, value, serializer);
        }
    }
}
