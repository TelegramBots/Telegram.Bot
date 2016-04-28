using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a location on a map. By default, the location will be sent by the user. Alternatively, you can use input_message_content to send a message with the specified content instead of the location.
    /// </summary>
    /// <remarks>
    /// This will only work in Telegram versions released after 9 April, 2016. Older clients will ignore them.
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQueryResultLocation : InlineQueryResultNew
    {
        /// <summary>
        /// Latitude of the location in degrees
        /// </summary>
        [JsonProperty("latitude", Required = Required.Always)]
        public float Latitude { get; set; }

        /// <summary>
        /// Longitude of the location in degrees
        /// </summary>
        [JsonProperty("longitude", Required = Required.Always)]
        public float Longitude { get; set; }
    }
}
