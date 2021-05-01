using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Use this method to edit a non-primary invite link created by the bot. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class EditChatInviteLinkRequest : RequestBase<ChatInviteLink>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ChatId ChatId { get; }

        /// <summary>
        /// The invite link to edit
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string InviteLink { get; }

        /// <summary>
        /// Point in time when the link will expire
        /// </summary>
        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        ///	Maximum number of users that can be members of the chat simultaneously after joining the chat via this invite link; 1-99999
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? MemberLimit { get; set; }

        /// <summary>
        /// Initializes a new request with chat_id and photo
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="inviteLink">The invite link to edit</param>
        public EditChatInviteLinkRequest(ChatId chatId, string inviteLink)
            : base("editChatInviteLink")
        {
            ChatId = chatId;
            InviteLink = inviteLink;
        }
    }
}
