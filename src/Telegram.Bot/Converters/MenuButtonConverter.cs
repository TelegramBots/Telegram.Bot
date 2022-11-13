using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class MenuButtonConverter : JsonConverter<MenuButton?>
{
    public override MenuButton? ReadJson(
        JsonReader reader,
         Type objectType,
         MenuButton? existingValue,
         bool hasExistingValue,
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
        var value = (MenuButton)Activator.CreateInstance(actualType)!;
        serializer.Populate(jo.CreateReader(), value);

        return value;
    }

    public override void WriteJson(JsonWriter writer, MenuButton? value, JsonSerializer serializer)
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
}
