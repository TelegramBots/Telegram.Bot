using System.Threading;
using Telegram.Bot.Exceptions;

#pragma warning disable CS1591

namespace Telegram.Bot.Serialization;

public sealed class InputFileConverter : JsonConverter<InputFile?>
{
    public static string? AllowLocalFilesUnder { get; set; } // if not empty, should end with a path separator
    public static readonly AsyncLocal<List<InputFileStream>?> Attachments = new();

    public override bool CanConvert(Type typeToConvert) => typeof(InputFile).IsAssignableFrom(typeToConvert);

    public override InputFile? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonElement.TryParseValue(ref reader, out var element))
            throw new JsonException("Could not read JSON value");

        var value = element.ToString();

        if (value is null)
            return null;
        if (value.StartsWith("attach://", StringComparison.OrdinalIgnoreCase))
            if (Attachments.Value?.Find(a => a.FileName?.StartsWith(value, StringComparison.Ordinal) == true) is { } ifs)
                return new InputFileStream(ifs.Content, ifs.FileName![(value.Length + 1)..]);
            else
                return new InputFileStream(Stream.Null, value[9..]);
        else if (value.StartsWith("file://", StringComparison.OrdinalIgnoreCase))
            if (AllowLocalFilesUnder == null)
                throw new ApiRequestException("file:// URI not allowed", 400);
            else if (Path.GetFullPath(new Uri(value).LocalPath) is { } path && path.StartsWith(AllowLocalFilesUnder, StringComparison.OrdinalIgnoreCase))
                return new InputFileStream(File.OpenRead(path), Path.GetFileName(path));
            else
                throw new ApiRequestException("file:// path is not under allowed folder", 400);

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
                var attachments = Attachments.Value ??= [];
                writer.WriteStringValue($"attach://{attachments.Count}");
                attachments.Add(file);
                break;
            default:
                throw new JsonException("File Type not supported");
        }
    }
}
