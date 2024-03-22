using System.IO;

namespace Telegram.Bot.Serialization;

internal class InputFileConverter : JsonConverter<InputFile?>
{
    public override bool CanConvert(Type typeToConvert) => typeof(InputFile).IsAssignableFrom(typeToConvert);

    public override InputFile? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonElement.TryParseValue(ref reader, out var element))
            throw new JsonException("Could not read JSON value");

        var value = element.ToString();

        if (value is null)
            return null;
        if (value.StartsWith("attach://", StringComparison.OrdinalIgnoreCase))
            return new InputFileStream(Stream.Null, value.Substring(9));

        return Uri.TryCreate(value, UriKind.Absolute, out var url)
            ? new InputFileUrl(url)
            : new InputFileId(value);
    }

    public override void Write(Utf8JsonWriter writer, InputFile? value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case InputFileId file:
                writer.WriteStringValue(file.Id);
                break;
            case InputFileUrl file:
                writer.WriteStringValue(file.Url.ToString());
                break;
            case InputFileStream file:
                writer.WriteStringValue($"attach://{file.FileName}");
                break;
            default:
                throw new JsonException("File Type not supported");
        }
    }
}
