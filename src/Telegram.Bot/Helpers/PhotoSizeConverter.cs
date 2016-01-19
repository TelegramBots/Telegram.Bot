using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;

namespace Telegram.Bot.Helpers
{
    internal class PhotoSizeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
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
        {
            return (typeof(PhotoSize) == objectType);
        }
    }
}
