using System;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Telegram.Bot.Helpers
{
    internal class InlineQueryResultTypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var resultType = (InlineQueryResultType)value;

            writer.WriteValue(resultType.ToTypeString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            InlineQueryResultType resultType;

            var value = reader.Value.ToString().Replace("_", "");

            Enum.TryParse(value, true, out resultType);

            return resultType;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (InlineQueryResultType);
        }
    }
}
