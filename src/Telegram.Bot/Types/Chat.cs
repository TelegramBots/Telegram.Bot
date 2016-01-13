using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a chat.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Chat
    {
        /// <summary>
        /// Unique identifier for this chat, not exceeding 1e13 by absolute value
        /// </summary>
        [JsonProperty(PropertyName = "id", Required = Required.Always)]
        public long Id { get; internal set; }

        /// <summary>
        /// Type of chat, can be either "Private", or "Group", or "Channel"
        /// </summary>
        [JsonProperty(PropertyName = "type", Required = Required.Always)]
        public ChatType Type { get; internal set; }

        /// <summary>
        /// Optional. Title, for channels and group chats
        /// </summary>
        [JsonProperty(PropertyName = "title", Required = Required.Default)]
        public string Title { get; internal set; }

        /// <summary>
        /// Optional. Username, for private chats and channels if available
        /// </summary>
        [JsonProperty(PropertyName = "username", Required = Required.Default)]
        public string Username { get; internal set; }

        /// <summary>
        /// Optional. First name of the other party in a private chat
        /// </summary>
        [JsonProperty(PropertyName = "first_name", Required = Required.Default)]
        public string FirstName { get; internal set; }

        /// <summary>
        /// Optional. Last name of the other party in a private chat
        /// </summary>
        [JsonProperty(PropertyName = "last_name", Required = Required.Default)]
        public string LastName { get; internal set; }
    }
}
