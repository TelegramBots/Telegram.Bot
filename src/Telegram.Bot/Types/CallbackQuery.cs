using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents an incoming callback query from a callback button in an inline keyboard. If the button that originated the query was attached to a message sent by the bot, the field message will be presented. If the button was attached to a message sent via the bot (in inline mode), the field inline_message_id will be presented.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class CallbackQuery
    {
        /// <summary>
        /// Unique identifier for this query
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        /// <summary>
        /// Sender
        /// </summary>
        [JsonProperty("from", Required = Required.Always)]
        public User From { get; set; }

        /// <summary>
        /// Optional. Message with the callback button that originated the query. Note that message content and message date will not be available if the message is too old
        /// </summary>
        [JsonProperty("message", Required = Required.Default)]
        public Message Message { get; set; }

        /// <summary>
        /// Optional. Identifier of the message sent via the bot in inline mode, that originated the query
        /// </summary>
        [JsonProperty("inline_message_id", Required = Required.Default)]
        public string InlineMessageId { get; set; }

        /// <summary>
        /// Data associated with the callback button. Be aware that a bad client can send arbitrary data in this field
        /// </summary>
        [JsonProperty("data", Required = Required.Always)]
        public string Data { get; set; }
    }
}
