using System.Linq;
using System.Reflection;
using System.Text.Json;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters.SystemTextJson;

internal class MenuButtonConverter : System.Text.Json.Serialization.JsonConverter<MenuButton>
{
    static readonly TypeInfo BaseType = typeof(ChatMember).GetTypeInfo();

    public override bool CanConvert(Type typeToConvert) => BaseType.IsAssignableFrom(typeToConvert.GetTypeInfo());

    public override MenuButton? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        JsonDocument doc = JsonDocument.ParseValue(ref reader);
        JsonElement typeElement = doc.RootElement.EnumerateObject().First(obj => string.Equals(obj.Name, "type", StringComparison.Ordinal)).Value;
        var type = typeElement.Deserialize<MenuButtonType>(options);

        Type actualType = type switch
        {
            MenuButtonType.Default => typeof(MenuButtonDefault),
            MenuButtonType.Commands => typeof(MenuButtonCommands),
            MenuButtonType.WebApp => typeof(MenuButtonWebApp),
            _ => throw new NotSupportedException($"Unknown menu button type value of '{typeElement.GetString()}'")
        };

        return doc.Deserialize(actualType, options) as MenuButton;
    }

    public override void Write(Utf8JsonWriter writer, MenuButton value, JsonSerializerOptions options)
    {
        System.Text.Json.JsonSerializer.Serialize(writer, value, options);
    }
}
