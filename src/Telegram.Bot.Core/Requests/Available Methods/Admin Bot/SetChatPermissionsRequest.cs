using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Use this method to set default chat permissions for all members. The bot must be an administrator in the group or a supergroup for this to work and must have the can_restrict_members admin rights. Returns True on success.
    /// </summary>
    public class SetChatPermissionsRequest : RequestBase<bool>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target supergroup (in the format @supergroupusername)
        /// </summary>
        public ChatId ChatId { get; }

        /// <summary>
        /// New default chat permissions
        /// </summary>
        public ChatPermissions Permissions { get; }

        /// <summary>
        /// Initializes a new request with chatId and new default permissions
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="permissions">New default permissions</param>
        public SetChatPermissionsRequest(ChatId chatId, ChatPermissions permissions,
                                         ITelegramBotJsonConverter jsonConverter)
            : base(jsonConverter, "setChatPermissions")
        {
            ChatId = chatId;
            Permissions = permissions;
        }
    }
}
