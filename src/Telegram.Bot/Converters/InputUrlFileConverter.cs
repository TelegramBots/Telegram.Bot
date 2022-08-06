using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Telegram.Bot.Types;

namespace Telegram.Bot.Converters;

internal class InputUrlFileConverter : JsonConverter<InputUrlFile>
{
    public override void WriteJson(JsonWriter writer, InputUrlFile inputUrlFile, JsonSerializer serializer)
    {
        writer.WriteValue(inputUrlFile.value);
    }

    public override InputUrlFile ReadJson(
        JsonReader reader,
        Type objectType,
        InputUrlFile existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var value = JToken.ReadFrom(reader).Value<string>();
        if (value is not { }) throw new JsonSerializationException();

        return new InputUrlFile(value);
    }
}
