using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object contains information about a poll.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Poll
    {
        /// <summary>
        /// Unique poll identifier
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }

        /// <summary>
        /// Poll question, 1-255 characters
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Question { get; set; }

        /// <summary>
        /// List of poll options
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public PollOption[] Options { get; set; }

        /// <summary>
        /// True, if the poll is closed
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public bool IsClosed { get; set; }
    }
}
