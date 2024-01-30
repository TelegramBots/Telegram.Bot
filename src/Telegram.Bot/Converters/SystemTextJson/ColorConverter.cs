using System.Text.Json;

namespace Telegram.Bot.Converters.SystemTextJson;

internal class NullableColorConverter : System.Text.Json.Serialization.JsonConverter<Color?>
{
    public override Color? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonElement.ParseValue(ref reader).TryGetInt32(out int value)
            ? new Color(value)
            : new Color?();
    }

    public override void Write(Utf8JsonWriter writer, Color? value, JsonSerializerOptions options)
    {
        if (value?.ToInt() is { } colorValue)
        {
            writer.WriteNumberValue(colorValue);
        }
    }
}

internal class ColorConverter : System.Text.Json.Serialization.JsonConverter<Color>
{
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new Color(JsonElement.ParseValue(ref reader).GetInt32());
    }

    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.ToInt());
    }
}
