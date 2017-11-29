using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// A placeholder, currently holds no information.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn,
                NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class CallbackGame
    {
    }
}
