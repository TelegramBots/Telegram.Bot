using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SetChatAdministratorCustomTitleAsync" /> method.
    /// </summary>
    public class SetChatAdministratorCustomTitleParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the target chat or username of the target channel
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Unique identifier of the target user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     New custom title for the administrator; 0-16 characters, emoji are not allowed
        /// </summary>
        public string CustomTitle { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        public SetChatAdministratorCustomTitleParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="UserId" /> property.
        /// </summary>
        /// <param name="userId">Unique identifier of the target user</param>
        public SetChatAdministratorCustomTitleParameters WithUserId(int userId)
        {
            UserId = userId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="CustomTitle" /> property.
        /// </summary>
        /// <param name="customTitle">New custom title for the administrator; 0-16 characters, emoji are not allowed</param>
        public SetChatAdministratorCustomTitleParameters WithCustomTitle(string customTitle)
        {
            CustomTitle = customTitle;
            return this;
        }
    }
}
