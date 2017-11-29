using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Telegram.Bot.Helpers;

namespace Telegram.Bot.Converters
{
    internal class UnixDateTimeConverter : DateTimeConverterBase
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            switch (value)
            {
                case null:
                    writer.WriteNull();
                    break;
                case DateTime dateTime:
                    writer.WriteValue(dateTime.ToUnixTime());
                    break;
                default:
                    throw new Exception("Expected date object value.");
            }
        }

        /// <summary>
        ///   Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                if (Nullable.GetUnderlyingType(objectType) != null)
                    return default;

                return null;
            }

            if (reader.TokenType != JsonToken.Integer)
                throw new Exception("Wrong Token Type");

            var ticks = (long)reader.Value;

            return ticks.FromUnixTime();
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
            => objectType == typeof(DateTime) || objectType == typeof(DateTime?);
    }
}
