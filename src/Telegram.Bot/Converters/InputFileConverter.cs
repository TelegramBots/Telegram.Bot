using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Telegram.Bot.Converters;

internal class InputFileConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) =>
        objectType.GetTypeInfo().IsSubclassOf(typeof(IInputFile));

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        writer.WriteValue(value switch
        {
            InputFileId file  => file.Id,
            InputFileUrl file => file.Url,
            InputFile file    => $"attach://{file.FileName}",
            _                 => throw new NotSupportedException("File Type not supported")
        });
    }

    public override object ReadJson(
        JsonReader reader,
        Type objectType,
        object? existingValue,
        JsonSerializer serializer)
    {
        var value = JToken.ReadFrom(reader).Value<string>();

        if (value is null) { return null!; }
        if (value.StartsWith("attach://", StringComparison.InvariantCulture))
        {
            return new InputFile(Stream.Null, value.Substring(9));
        }

        return Uri.IsWellFormedUriString(value, UriKind.Absolute)
            ? new InputFileUrl(value)
            : new InputFileId(value);
    }
}
