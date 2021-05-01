using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents the content of a service message, sent whenever a user in the chat triggers a proximity alert set by another user.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ProximityAlertTriggered
    {
        /// <summary>
        /// User that triggered the alert
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public User Traveler { get; set; }

        /// <summary>
        /// User that set the alert
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public User Watcher { get; set; }

        /// <summary>
        /// The distance between the users
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int Distance { get; set; }
    }
}
