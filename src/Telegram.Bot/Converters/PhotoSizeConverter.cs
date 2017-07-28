using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;

namespace Telegram.Bot.Converters
{
    internal class PhotoSizeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var photoSize = (PhotoSize)value;
            var jObj = new JObject
            {
                ["file_id"] = photoSize.FileId,
                ["width"] = photoSize.Width,
                ["height"] = photoSize.Height,
                ["file_size"] = photoSize.FileSize,
            };
            jObj.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);

            if (!jObject.HasValues) return null;

            var photoSize = new PhotoSize();
            serializer.Populate(jObject.CreateReader(), photoSize);
            return photoSize;
        }

        public override bool CanConvert(Type objectType)
            => typeof(PhotoSize) == objectType;
    }
}
