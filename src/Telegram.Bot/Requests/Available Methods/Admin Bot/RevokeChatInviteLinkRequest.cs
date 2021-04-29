using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Use this method to revoke an invite link created by the bot. If the primary link is revoked, a new link is automatically generated. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class RevokeChatInviteLinkRequest : RequestBase<ChatInviteLink>
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
        /// Initializes a new request with chat_id and photo
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="inviteLink">The invite link to edit</param>
        public RevokeChatInviteLinkRequest(ChatId chatId, string inviteLink)
            : base("revokeChatInviteLink")
        {
            ChatId = chatId;
            InviteLink = inviteLink;
        }
    }
}
