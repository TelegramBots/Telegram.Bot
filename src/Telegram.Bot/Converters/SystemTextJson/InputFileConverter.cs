using System.IO;
using System.Reflection;
using System.Text.Json;

namespace Telegram.Bot.Converters.SystemTextJson;

internal class InputFileConverter : System.Text.Json.Serialization.JsonConverter<InputFile?>
{
    public override bool CanConvert(Type typeToConvert) =>
        typeToConvert.GetTypeInfo().IsSubclassOf(typeof(InputFileStream));

    public override InputFile? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = JsonElement.ParseValue(ref reader).GetString();

        if (value is null) { return null; }
        if (value.StartsWith("attach://", StringComparison.OrdinalIgnoreCase))
        {
            return new InputFileStream(Stream.Null, value.Substring(9));
        }

        return Uri.TryCreate(value, UriKind.Absolute, out var url)
            ? new InputFileUrl(url)
            : new InputFileId(value);
    }

    public override void Write(Utf8JsonWriter writer, InputFile? value, JsonSerializerOptions options)
    {
        if (value == null)
            writer.WriteNullValue();
        else
            writer.WriteStringValue(value switch
            {
                InputFileId file => file.Id,
                InputFileUrl file => file.Url.ToString(),
                InputFileStream file => $"attach://{file.FileName}",
                _ => throw new NotSupportedException("File Type not supported"),
            });
    }
}
