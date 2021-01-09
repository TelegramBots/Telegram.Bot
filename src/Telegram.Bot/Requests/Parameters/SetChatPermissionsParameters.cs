using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SetChatPermissionsAsync" /> method.
    /// </summary>
    public class SetChatPermissionsParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the target chat or username of the target channel
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     New default permissions
        /// </summary>
        public ChatPermissions Permissions { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        public SetChatPermissionsParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Permissions" /> property.
        /// </summary>
        /// <param name="permissions">New default permissions</param>
        public SetChatPermissionsParameters WithPermissions(ChatPermissions permissions)
        {
            Permissions = permissions;
            return this;
        }
    }
}