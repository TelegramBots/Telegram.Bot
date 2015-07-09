using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;

namespace Telegram.Bot.Helpers
{
    class ConversationConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            var jObject = JObject.Load(reader);

            IConversation conversation;

            if (jObject["title"] != null)
            {
                conversation = new GroupChat();
            }
            else if (jObject["first_name"] != null)
            {
                conversation = new User();
            }
            else
            {
                throw new JsonReaderException("Required properties not found");
            }
            
            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), conversation);

            return conversation;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (IConversation).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}
