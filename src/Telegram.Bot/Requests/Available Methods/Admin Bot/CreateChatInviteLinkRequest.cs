using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Use this method to create an additional invite link for a chat. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights. The link can be revoked using the method <see cref="RevokeChatInviteLinkRequest"/>.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class CreateChatInviteLinkRequest : RequestBase<ChatInviteLink>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ChatId ChatId { get; }

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
        public CreateChatInviteLinkRequest(ChatId chatId)
            : base("createChatInviteLink")
        {
            ChatId = chatId;
        }
    }
}
