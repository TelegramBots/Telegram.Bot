namespace Telegram.Bot.Serialization;

public class BotCommandScopeConverter : JsonConverter<BotCommandScope?>
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(BotCommandScope);

    public override BotCommandScope? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader, out var element))
        {
            throw new JsonException("Could not read JSON value");
        }

        try
        {
            var root = element.RootElement;
            var type = root.GetProperty("type").GetString() ?? throw new JsonException("Type property not found");

            switch (type)
            {
                case "default":
                    return JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.BotCommandScopeDefault);
                case "all_private_chats":
                    return JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.BotCommandScopeAllPrivateChats);
                case "all_group_chats":
                    return JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.BotCommandScopeAllGroupChats);
                case "all_chat_administrators":
                    return JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.BotCommandScopeAllChatAdministrators);
                case "chat":
                    return JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.BotCommandScopeChat);
                case "chat_administrators":
                    return JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.BotCommandScopeChatAdministrators);
                case "chat_member":
                    return JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.BotCommandScopeChatMember);
                default:
                    throw new JsonException($"Unsupported bot command scope type: {type}");
            }
        }
        finally
        {
            element.Dispose();
        }
    }

    public override void Write(Utf8JsonWriter writer, BotCommandScope? value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case BotCommandScopeDefault defaultScope:
                writer.WriteRawValue(JsonSerializer.Serialize(defaultScope, TelegramBotClientJsonSerializerContext.Instance.BotCommandScopeDefault));
                break;
            case BotCommandScopeAllPrivateChats allPrivateChats:
                writer.WriteRawValue(JsonSerializer.Serialize(allPrivateChats, TelegramBotClientJsonSerializerContext.Instance.BotCommandScopeAllPrivateChats));
                break;
            case BotCommandScopeAllGroupChats allGroupChats:
                writer.WriteRawValue(JsonSerializer.Serialize(allGroupChats, TelegramBotClientJsonSerializerContext.Instance.BotCommandScopeAllGroupChats));
                break;
            case BotCommandScopeAllChatAdministrators allChatAdministrators:
                writer.WriteRawValue(JsonSerializer.Serialize(allChatAdministrators, TelegramBotClientJsonSerializerContext.Instance.BotCommandScopeAllChatAdministrators));
                break;
            case BotCommandScopeChat chat:
                writer.WriteRawValue(JsonSerializer.Serialize(chat, TelegramBotClientJsonSerializerContext.Instance.BotCommandScopeChat));
                break;
            case BotCommandScopeChatAdministrators chatAdministrators:
                writer.WriteRawValue(JsonSerializer.Serialize(chatAdministrators, TelegramBotClientJsonSerializerContext.Instance.BotCommandScopeChatAdministrators));
                break;
            case BotCommandScopeChatMember chatMember:
                writer.WriteRawValue(JsonSerializer.Serialize(chatMember, TelegramBotClientJsonSerializerContext.Instance.BotCommandScopeChatMember));
                break;
            case null:
                writer.WriteNullValue();
                break;
            default:
                throw new JsonException("Unsupported bot command scope type");
        }
    }
}
