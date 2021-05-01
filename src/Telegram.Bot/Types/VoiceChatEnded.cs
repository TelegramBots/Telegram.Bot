using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a service message about a voice chat ended in the chat.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class VoiceChatEnded
    {
        /// <summary>
        /// Voice chat duration; in seconds
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int Duration { get; set; }
    }
}
