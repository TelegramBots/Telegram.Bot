using System;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters
{
    // ToDo: Unit test serialize/deserialize
    internal class InputMediaTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => typeof(InputMediaType) == objectType;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var inputMediaType = (InputMediaType)value;
            switch (inputMediaType.FileType)
            {
                case FileType.Id:
                    writer.WriteValue(inputMediaType.FileId);
                    break;
                case FileType.Url:
                    writer.WriteValue(inputMediaType.Url);
                    break;
                case FileType.Stream:
                    writer.WriteValue($"attach://{inputMediaType.FileName}");
                    break;
                default:
                    throw new NotSupportedException("File Type not supported");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.ReadAsString();

            if (Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out Uri uri))
            {
                return new FileToSend(uri);
            }
            else
            {
                return new FileToSend(value);
            }
        }
    }
}
