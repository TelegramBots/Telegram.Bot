using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Telegram.Bot.Types;

namespace Telegram.Bot.Converters;

internal class ChatIdConverter : JsonConverter<ChatId>
{
    public override void WriteJson(JsonWriter writer, ChatId? value, JsonSerializer serializer)
    {
        if (value is null)
        {
            writer.WriteNull();
        }
        else if (value.Username is not null)
        {
            writer.WriteValue(value.Username);
        }
        else
        {
            writer.WriteValue(value.Identifier);
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
