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
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; internal set; }

        /// <summary>
        /// Sender
        /// </summary>
        [JsonProperty("from", Required = Required.Always)]
        public User From { get; internal set; }

        /// <summary>
        /// Text of the query
        /// </summary>
        [JsonProperty("query", Required = Required.Always)]
        public string Query { get; internal set; }

        /// <summary>
        /// Optional. Sender location, only for bots that request user location
        /// </summary>
        [JsonProperty("location", Required = Required.Default)]
        public Location Location { get; internal set; }

        /// <summary>
        /// Offset of the results to be returned, can be controlled by the bot
        /// </summary>
        [JsonProperty("offset", Required = Required.Always)]
        public string Offset { get; internal set; }
    }
}
