using System;
using System.Linq;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Telegram.Bot.Helpers
{
    internal class MessageEntityTypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(MessageEntityTypeExtensions.StringMap[(MessageEntityType)value]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return MessageEntityTypeExtensions.StringMap.Where(type => type.Value == reader.Value.ToString()).Select(kv => kv.Key).FirstOrDefault();
        }

        public override bool CanConvert(Type objectType) => objectType == typeof (MessageEntityType);
    }
}
