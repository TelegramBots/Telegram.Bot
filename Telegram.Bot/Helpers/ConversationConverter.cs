using System;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;

namespace Telegram.Bot.Helpers
{
    class ConversationConverter : JsonCreationConverter<IConversation>
    {
        protected override IConversation Create(Type objectType, JObject jObject)
        {
            if (jObject["title"] != null)
                return new GroupChat();

            if (jObject["first_name"] != null)
                return new User();

            throw new FormatException();
        }
    }
}
