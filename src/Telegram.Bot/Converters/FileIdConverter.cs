using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Telegram.Bot.Types;

namespace Telegram.Bot.Converters;

internal class FileIdConverter : JsonConverter<InputFileId>
{
    public override void WriteJson(JsonWriter writer, InputFileId fileId, JsonSerializer serializer)
    {
        writer.WriteValue(fileId.value);
    }

    public override InputFileId ReadJson(
        JsonReader reader,
        Type objectType,
        InputFileId existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var value = JToken.ReadFrom(reader).Value<string>();

        if (value is not { }) throw new JsonSerializationException();

        return new InputFileId(value);
    }
}
