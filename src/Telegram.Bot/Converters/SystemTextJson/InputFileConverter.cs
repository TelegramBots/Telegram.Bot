using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace Telegram.Bot.Converters.SystemTextJson;

internal class InputFileConverter : JsonConverter<IInputFile>
{
    public override bool CanConvert(Type typeToConvert) =>
        typeToConvert.GetTypeInfo().IsSubclassOf(typeof(InputFileStream));

    public override IInputFile Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = JsonElement.ParseValue(ref reader).GetString();
        if (value is null)
        {
            return new InputFileStream(Stream.Null);
        }

        return Uri.TryCreate(value, UriKind.Absolute, out _)
            ? new InputOnlineFile(value)
            : new InputTelegramFile(value);
    }

    public override void Write(Utf8JsonWriter writer, IInputFile value, JsonSerializerOptions options)
    {
        switch (value.FileType)
        {
            case FileType.Stream:
                writer.WriteNullValue();
                break;
            case FileType.Id when value is InputTelegramFile file:
                writer.WriteStringValue(file.FileId);
                break;
            case FileType.Url when value is InputOnlineFile file:
                writer.WriteStringValue(file.Url);
                break;
            default:
                throw new NotSupportedException("File Type is not supported");
        }
    }
}
