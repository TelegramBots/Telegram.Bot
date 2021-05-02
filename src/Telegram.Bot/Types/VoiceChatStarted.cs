using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a service message about a voice chat started in the chat. Currently holds no information.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class VoiceChatStarted
    {
    }
}
