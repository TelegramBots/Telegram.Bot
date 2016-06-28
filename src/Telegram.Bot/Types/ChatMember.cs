using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object contains information about one member of the chat.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ChatMember
    {
        /// <summary>
        /// Information about the user
        /// </summary>
        [JsonProperty("user", Required = Required.Always)]
        public User User { get; internal set; }

        /// <summary>
        /// The member's status in the chat.
        /// </summary>
        [JsonProperty("status", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ChatMemberStatus Status { get; internal set; }
    }
}
