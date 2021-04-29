using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a chat.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ChatMemberUpdated
    {
        /// <summary>
        /// Chat the user belongs to
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Chat Chat { get; set; }

        /// <summary>
        /// Performer of the action, which resulted in the change
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public User From { get; set; }

        /// <summary>
        /// Date the change was done
        /// </summary>
        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty(Required = Required.Always)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Previous information about the chat member
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ChatMember OldChatMember { get; set; }

        /// <summary>
        /// New information about the chat member
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ChatMember NewChatMember { get; set; }

        /// <summary>
        /// Optional. Chat invite link, which was used by the user to join the chat; for joining by invite link events only.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ChatInviteLink InviteLink { get; set; }
    }
}
