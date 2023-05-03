using System.IO;
using Newtonsoft.Json.Linq;

namespace Telegram.Bot.Converters;

internal class InputFileConverter : JsonConverter<InputFile?>
{
    public override void WriteJson(JsonWriter writer, InputFile? value, JsonSerializer serializer)
    {
        writer.WriteValue(value switch
        {
            InputFileId file     => file.Id,
            InputFileUrl file    => file.Url,
            InputFileStream file => $"attach://{file.FileName}",
            _                    => throw new NotSupportedException("File Type not supported"),
        });
    }

    public override InputFile? ReadJson(
        JsonReader reader,
        Type objectType,
        InputFile? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var value = JToken.ReadFrom(reader).Value<string>();

        if (value is null) { return null; }
        if (value.StartsWith("attach://", StringComparison.OrdinalIgnoreCase))
        {
            return new InputFileStream(Stream.Null, value.Substring(9));
        }

        return Uri.TryCreate(value, UriKind.Absolute, out var url)
            ? new InputFileUrl(url)
            : new InputFileId(value);
    }
}
