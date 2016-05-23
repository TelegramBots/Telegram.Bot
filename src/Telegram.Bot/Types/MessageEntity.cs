using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents one special entity in a text message. For example, hashtags, usernames, URLs, etc.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class MessageEntity
    {
        /// <summary>
        /// Type of the entity
        /// </summary>
        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageEntityType Type { get; internal set; }

        /// <summary>
        /// Offset in UTF-16 code units to the start of the entity
        /// </summary>
        [JsonProperty("offset", Required = Required.Always)]
        public int Offset { get; internal set; }

        /// <summary>
        /// Length of the entity in UTF-16 code units
        /// </summary>
        [JsonProperty("length", Required = Required.Always)]
        public int Length { get; internal set; }

        /// <summary>
        /// Optional. For "text_link" only, url that will be opened after user taps on the text
        /// </summary>
        [JsonProperty("url", Required = Required.Default)]
        public string Url { get; internal set; }

        /// <summary>
        /// Optional. For "text_mention" only, the mentioned user
        /// </summary>
        [JsonProperty("user", Required = Required.Default)]
        public User User { get; internal set; }
    }
}
