using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters.SystemTextJson;

internal class ChatMemberConverter : JsonConverter<ChatMember>
{
    static readonly TypeInfo BaseType = typeof(ChatMember).GetTypeInfo();

    public override bool CanConvert(Type typeToConvert) => BaseType.IsAssignableFrom(typeToConvert.GetTypeInfo());

    public override ChatMember Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        JsonDocument doc = JsonDocument.ParseValue(ref reader);
        JsonElement statusElement = doc.RootElement.EnumerateObject().First(obj => obj.Name == "status").Value;
        var status = statusElement.Deserialize<ChatMemberStatus>(options);

        Type actualType = status switch
        {
            ChatMemberStatus.Creator => typeof(ChatMemberOwner),
            ChatMemberStatus.Administrator => typeof(ChatMemberAdministrator),
            ChatMemberStatus.Member => typeof(ChatMemberMember),
            ChatMemberStatus.Left => typeof(ChatMemberLeft),
            ChatMemberStatus.Kicked => typeof(ChatMemberBanned),
            ChatMemberStatus.Restricted => typeof(ChatMemberRestricted),
            _ => throw new NotSupportedException($"Unknown chat member status value of '{statusElement.GetString()}'")
        };

        return (ChatMember) doc.Deserialize(actualType, options)!;
    }

    public override void Write(Utf8JsonWriter writer, ChatMember value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
