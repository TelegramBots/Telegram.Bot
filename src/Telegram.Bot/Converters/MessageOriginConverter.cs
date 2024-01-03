using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Telegram.Bot.Converters;

internal class MessageOriginConverter : JsonConverter
{
    static readonly TypeInfo BaseType = typeof(MessageOrigin).GetTypeInfo();

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
            "user"        => typeof(MessageOriginUser),
            "hidden_user" => typeof(MessageOriginHiddenUser),
            "chat"        => typeof(MessageOriginChat),
            "channel"     => typeof(MessageOriginChannel),
            _             => throw new JsonSerializationException($"Unknown message origin type value of '{jo["type"]}'")
        };

        // Remove status because status property only has getter
        jo.Remove("type");
        var value = Activator.CreateInstance(actualType)!;
        serializer.Populate(jo.CreateReader(), value);

        return value;
    }
}
