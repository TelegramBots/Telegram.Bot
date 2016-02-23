using System;
using System.Linq;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Telegram.Bot.Helpers
{
    internal class ParseModeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(ParseModeExtensions.StringMap[(ParseMode)value]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return ParseModeExtensions.StringMap.FirstOrDefault(mode => mode.Value == reader.Value.ToString());
        }

        public override bool CanConvert(Type objectType) => objectType == typeof (ParseMode);
    }
}
