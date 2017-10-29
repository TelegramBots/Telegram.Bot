using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a result of an <see cref="InlineQuery"/> that was chosen by the <see cref="User"/> and sent to their chat partner.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ChosenInlineResult
    {
        /// <summary>
        /// The unique identifier for the result that was chosen.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string ResultId { get; set; }

        /// <summary>
        /// The user that chose the result.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public User From { get; set; }

        /// <summary>
        /// Optional. Sender location, only for bots that require user location
        /// </summary>
        [JsonProperty]
        public Location Location { get; set; }

        /// <summary>
        /// Optional. Identifier of the sent inline message. Available only if there is an inline keyboard attached to the message. Will be also received in callback queries and can be used to edit the message.
        /// </summary>
        [JsonProperty]
        public string InlineMessageId { get; set; }

        /// <summary>
        /// The query that was used to obtain the result.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Query { get; set; }
    }
}
