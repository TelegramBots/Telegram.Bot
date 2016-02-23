using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a result of an inline query that was chosen by the user and sent to their chat partner.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ChosenInlineResult
    {
        /// <summary>
        /// The unique identifier for the result that was chosen.
        /// </summary>
        [JsonProperty("result_id", Required = Required.Always)]
        public string ResultId { get; internal set; }

        /// <summary>
        /// The user that chose the result.
        /// </summary>
        [JsonProperty("from", Required = Required.Always)]
        public User From { get; internal set; }

        /// <summary>
        /// The query that was used to obtain the result.
        /// </summary>
        [JsonProperty("query", Required = Required.Always)]
        public string Query { get; internal set; }
    }
}
