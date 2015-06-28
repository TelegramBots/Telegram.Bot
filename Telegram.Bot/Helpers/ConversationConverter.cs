using System;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;

namespace Telegram.Bot.Helpers
{
    class ConversationConverter : JsonCreationConverter<Conversation>
    {
        protected override Conversation Create(Type objectType, JObject jObject)
        {
            if (FieldExists("title", jObject))
            {
                return new GroupChat();
            }
            return FieldExists("first_name", jObject) ? new User() : new Conversation();
        }

        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }
    }
}
