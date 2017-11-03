using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents an incoming inline query. When the user sends an empty query, your bot could return some default or trending results.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineQuery
    {
        /// <summary>
        /// Unique identifier for this query
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }

        /// <summary>
        /// Sender
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public User From { get; set; }

        /// <summary>
        /// Text of the query
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Query { get; set; }

        /// <summary>
        /// Optional. Sender location, only for bots that request user location
        /// </summary>
        [JsonProperty]
        public Location Location { get; set; }

        /// <summary>
        /// Offset of the results to be returned, can be controlled by the bot
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Offset { get; set; }
    }
}
