using System.Reflection;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class ChatBoostSourceConverter : JsonConverter
{
    static readonly TypeInfo BaseType = typeof(ChatBoostSource).GetTypeInfo();

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
        var type = jo["source"]?.ToObject<ChatBoostSourceType>();

        if (type is null)
        {
            return null;
        }

        var actualType = type switch
        {
            ChatBoostSourceType.Premium  => typeof(ChatBoostSourcePremium),
            ChatBoostSourceType.GiftCode => typeof(ChatBoostSourceGiftCode),
            ChatBoostSourceType.Giveaway => typeof(ChatBoostSourceGiveaway),
            _ => throw new JsonSerializationException($"Unknown chat boost source value of '{jo["source"]}'")
        };

        // Remove status because status property only has getter
        jo.Remove("source");
        var value = Activator.CreateInstance(actualType)!;
        serializer.Populate(jo.CreateReader(), value);

        return value;
    }
}
