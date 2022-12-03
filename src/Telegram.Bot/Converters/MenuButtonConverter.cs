using System.Reflection;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class MenuButtonConverter : JsonConverter
{
    static readonly TypeInfo BaseType = typeof(ChatMember).GetTypeInfo();

    public override bool CanWrite => false;
    public override bool CanRead => true;

    public override bool CanConvert(Type objectType) => BaseType.IsAssignableFrom(objectType.GetTypeInfo());

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
        var typeToken = jo["type"];
        var status = typeToken?.ToObject<MenuButtonType>();

        if (status is null)
        {
            return null;
        }

        var actualType = status switch
        {
            MenuButtonType.Default => typeof(MenuButtonDefault),
            MenuButtonType.Commands => typeof(MenuButtonCommands),
            MenuButtonType.WebApp => typeof(MenuButtonWebApp),
            _ => throw new JsonSerializationException($"Unknown menu button type value of '{typeToken}'")
        };

        // Remove status because status property only has getter
        jo.Remove("type");
        var value = Activator.CreateInstance(actualType)!;
        serializer.Populate(jo.CreateReader(), value);

        return value;
    }
}
