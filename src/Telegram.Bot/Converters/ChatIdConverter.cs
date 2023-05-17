using Newtonsoft.Json.Linq;

namespace Telegram.Bot.Converters;

internal class ChatIdConverter : JsonConverter<ChatId?>
{
    public override void WriteJson(JsonWriter writer, ChatId? value, JsonSerializer serializer)
    {
        switch (value)
        {
            case { Username: {} username }:
                writer.WriteValue(username);
                break;
            case { Identifier: {} identifier }:
                writer.WriteValue(identifier);
                break;
            case null:
                writer.WriteNull();
                break;
            default:
                throw new JsonSerializationException();
        }
    }

    public override ChatId? ReadJson(
        JsonReader reader,
        Type objectType,
        ChatId? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var value = JToken.ReadFrom(reader).Value<string>();
        return value is null ? null : new(value);
    }
}
