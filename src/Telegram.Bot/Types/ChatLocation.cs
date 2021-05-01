using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a location to which a chat is connected.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ChatLocation
    {
        /// <summary>
        /// The location to which the supergroup is connected. Can't be a live location.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Location Location { get; set; }

        /// <summary>
        /// Location address; 1-64 characters, as defined by the chat owner
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string String { get; set; }
    }
}
