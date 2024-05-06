namespace Telegram.Bot.Serialization;

public class ChatMemberConverter : JsonConverter<ChatMember?>
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(ChatMember);

    public override ChatMember? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader, out var element))
        {
            throw new JsonException("Could not read JSON value");
        }

        try
        {
            var root = element.RootElement;
            var status = root.GetProperty("status").GetString() ?? throw new JsonException("Status property not found");

            return status switch
            {
                "creator" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.ChatMemberOwner),
                "administrator" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.ChatMemberAdministrator),
                "member" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.ChatMemberMember),
                "restricted" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.ChatMemberRestricted),
                "left" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.ChatMemberLeft),
                "kicked" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.ChatMemberBanned),
                null => null,
                _ => throw new JsonException($"Unsupported chat member status: {status}")
            };
        }
        finally
        {
            element.Dispose();
        }
    }

    public override void Write(Utf8JsonWriter writer, ChatMember? value, JsonSerializerOptions options)
    {
        if (value is not null)
        {
            writer.WriteRawValue(value switch
            {
                ChatMemberOwner owner => JsonSerializer.Serialize(owner, TelegramBotClientJsonSerializerContext.Instance.ChatMemberOwner),
                ChatMemberAdministrator administrator => JsonSerializer.Serialize(administrator, TelegramBotClientJsonSerializerContext.Instance.ChatMemberAdministrator),
                ChatMemberMember member => JsonSerializer.Serialize(member, TelegramBotClientJsonSerializerContext.Instance.ChatMemberMember),
                ChatMemberRestricted restricted => JsonSerializer.Serialize(restricted, TelegramBotClientJsonSerializerContext.Instance.ChatMemberRestricted),
                ChatMemberLeft left => JsonSerializer.Serialize(left, TelegramBotClientJsonSerializerContext.Instance.ChatMemberLeft),
                ChatMemberBanned banned => JsonSerializer.Serialize(banned, TelegramBotClientJsonSerializerContext.Instance.ChatMemberBanned),
                _ => throw new JsonException("Unsupported chat member status")
            });
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
