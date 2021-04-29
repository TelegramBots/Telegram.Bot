using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents an invite link for a chat.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ChatInviteLink
    {
        /// <summary>
        /// The invite link. If the link was created by another chat administrator, then the second part of the link will be replaced with “…”.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string InviteLink { get; set; }

        /// <summary>
        /// Creator of the link
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public User Creator { get; set; }

        /// <summary>
        /// True, if the link is primary
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? IsPrimary { get; set; }

        /// <summary>
        /// True, if the link is revoked
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? IsRevoked { get; set; }

        /// <summary>
        /// Optional. Point in time (Unix timestamp) when the link will expire or has been expired
        /// </summary>
        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// Optional. Maximum number of users that can be members of the chat simultaneously after joining the chat via this invite link; 1-99999
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? MemberLimit { get; set; }
    }
}
