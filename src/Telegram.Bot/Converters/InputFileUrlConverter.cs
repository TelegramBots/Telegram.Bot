using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Telegram.Bot.Types;

namespace Telegram.Bot.Converters;

internal class InputFileUrlConverter : JsonConverter<InputFileUrl>
{
    public override void WriteJson(JsonWriter writer, InputFileUrl inputFileUrl, JsonSerializer serializer)
    {
        writer.WriteValue(inputFileUrl.value);
    }

    public override InputFileUrl ReadJson(
        JsonReader reader,
        Type objectType,
        InputFileUrl existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var value = JToken.ReadFrom(reader).Value<string>();
        if (value is not { }) throw new JsonSerializationException();

        return new InputFileUrl(value);
    }
}
