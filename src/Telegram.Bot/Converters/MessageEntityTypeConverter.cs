﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;

namespace Telegram.Bot.Converters
{
    internal class MessageEntityTypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var messageEntityType = (MessageEntityType)value;
            writer.WriteValue(messageEntityType.Value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string value = JToken.ReadFrom(reader).Value<string>();
            return value.ToMessageType();
        }

        public override bool CanConvert(Type objectType) => typeof(MessageEntityType) == objectType;
    }
}
