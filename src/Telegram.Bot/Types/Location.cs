using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a point on the map.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Location
    {
        /// <summary>
        /// Longitude as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "longitude", Required = Required.Always)]
        public float Longitude { get; internal set; }

        /// <summary>
        /// Latitude as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "latitude", Required = Required.Always)]
        public float Latitude { get; internal set; }
    }
}
