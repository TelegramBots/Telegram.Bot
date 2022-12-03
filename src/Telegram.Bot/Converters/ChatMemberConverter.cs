using System.Reflection;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class ChatMemberConverter : JsonConverter
{
    static readonly TypeInfo BaseType = typeof(ChatMember).GetTypeInfo();

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
        var status = jo["status"]?.ToObject<ChatMemberStatus>();

        if (status is null)
        {
            return null;
        }

        var actualType = status switch
        {
            ChatMemberStatus.Creator => typeof(ChatMemberOwner),
            ChatMemberStatus.Administrator => typeof(ChatMemberAdministrator),
            ChatMemberStatus.Member => typeof(ChatMemberMember),
            ChatMemberStatus.Left => typeof(ChatMemberLeft),
            ChatMemberStatus.Kicked => typeof(ChatMemberBanned),
            ChatMemberStatus.Restricted => typeof(ChatMemberRestricted),
            _ => throw new JsonSerializationException($"Unknown chat member status value of '{jo["status"]}'")
        };

        // Remove status because status property only has getter
        jo.Remove("status");
        var value = Activator.CreateInstance(actualType)!;
        serializer.Populate(jo.CreateReader(), value);

        return value;
    }
}
