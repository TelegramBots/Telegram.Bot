using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Use this method to set default chat permissions for all members. The bot must be an administrator in the group or a supergroup for this to work and must have the can_restrict_members admin rights. Returns True on success.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SetChatPermissionsRequest : RequestBase<bool>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ChatId ChatId { get; }

        /// <summary>
        /// New default chat permissions
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ChatPermissions Permissions { get; }

        /// <summary>
        /// Initializes a new request with chatId and new default permissions
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
        /// <param name="permissions">New default chat permissions</param>
        public SetChatPermissionsRequest(ChatId chatId, ChatPermissions permissions)
            : base("setChatPermissions")
        {
            ChatId = chatId;
            Permissions = permissions;
        }
    }
}
