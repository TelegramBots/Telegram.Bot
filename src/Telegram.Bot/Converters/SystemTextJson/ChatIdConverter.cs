using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Telegram.Bot.Types;

namespace Telegram.Bot.Converters.SystemTextJson;

internal class ChatIdConverter : JsonConverter<ChatId>
{
    public override ChatId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new ChatId(JsonElement.ParseValue(ref reader).GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, ChatId value, JsonSerializerOptions options)
    {
        if (value.Username != null)
        {
            writer.WriteStringValue(value.Username);
        }
        else if (value.Identifier == null)
        {
            writer.WriteNullValue();
        }
        else
        {
            writer.WriteNumberValue(value.Identifier.Value);
        }
    }
}
