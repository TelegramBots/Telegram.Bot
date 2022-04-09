using System;
using System.IO;
using System.Text.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters.SystemTextJson;

internal class InputMediaConverter : InputFileConverter
{
    public override bool CanConvert(Type typeToConvert) => typeof(InputMedia) == typeToConvert;

    public override void Write(Utf8JsonWriter writer, IInputFile value, JsonSerializerOptions options)
    {
        var inputMediaType = (InputMedia)value;
        switch (inputMediaType.FileType)
        {
            case FileType.Id:
            case FileType.Url:
                base.Write(writer, value, options);
                break;
            case FileType.Stream:
                writer.WriteStringValue($"attach://{inputMediaType.FileName}");
                break;
            default:
                throw new NotSupportedException("File Type not supported");
        }
    }

    public override IInputFile Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = JsonElement.ParseValue(ref reader).GetString();

        if (value is null)
            return null!;

        return value.StartsWith("attach://", StringComparison.InvariantCulture)
            ? new InputMedia(Stream.Null, value.Substring(9))
            : new InputMedia(value);
    }
}
