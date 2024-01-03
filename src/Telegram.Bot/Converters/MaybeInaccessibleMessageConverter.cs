using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Telegram.Bot.Converters;

internal class MaybeInaccessibleMessageConverter : JsonConverter
{
    static readonly TypeInfo BaseType = typeof(MaybeInaccessibleMessage).GetTypeInfo();

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
        var date = jo["date"]?.Value<int>();

        if (date is null)
        {
            return null;
        }

        var actualType = date switch
        {
            0 => typeof(Message),
            _ => typeof(InaccessibleMessage),
        };

        // Remove status because status property only has getter
        var value = Activator.CreateInstance(actualType)!;
        serializer.Populate(jo.CreateReader(), value);

        return value;
    }
}
