using Newtonsoft.Json;
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
        public User User { get; set; }

        /// <summary>
        /// The member's status in the chat.
        /// </summary>
        [JsonProperty("status", Required = Required.Always)]
        public ChatMemberStatus Status { get; set; }
    }
}
