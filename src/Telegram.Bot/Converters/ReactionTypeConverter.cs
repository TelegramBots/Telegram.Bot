using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Telegram.Bot.Converters;

internal class ReactionTypeConverter : JsonConverter
{
    static readonly TypeInfo BaseType = typeof(ReactionType).GetTypeInfo();

    public override bool CanWrite => false;
    public override bool CanRead => true;
    public override bool CanConvert(Type objectType) =>
        BaseType.IsAssignableFrom(objectType.GetTypeInfo());

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is null)
        {
            writer.WriteNull();
        }
        else
        {
            var jo = JObject.FromObject(value);
            jo.WriteTo(writer);
        }
    }

    public override object? ReadJson(
        JsonReader reader,
        Type objectType,
        object? existingValue,
        JsonSerializer serializer)
    {
        var jo = JObject.Load(reader);
        var type = jo["type"]?.Value<string>();

        if (type is null)
        {
            return null;
        }

        var actualType = type switch
        {
            "emoji"        => typeof(ReactionTypeEmoji),
            "custom_emoji" => typeof(ReactionTypeCustomEmoji),
            _              => throw new JsonSerializationException($"Unknown reaction type value of '{jo["type"]}'")
        };

        // Remove status because status property only has getter
        jo.Remove("type");
        var value = Activator.CreateInstance(actualType)!;
        serializer.Populate(jo.CreateReader(), value);

        return value;
    }
}
