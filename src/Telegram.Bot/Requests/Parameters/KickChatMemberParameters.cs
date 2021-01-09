using System;
using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.KickChatMemberAsync" /> method.
    /// </summary>
    public class KickChatMemberParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Unique identifier of the target user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// </summary>
        public DateTime UntilDate { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId"></param>
        public KickChatMemberParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="UserId" /> property.
        /// </summary>
        /// <param name="userId">Unique identifier of the target user</param>
        public KickChatMemberParameters WithUserId(int userId)
        {
            UserId = userId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="UntilDate" /> property.
        /// </summary>
        /// <param name="untilDate"></param>
        public KickChatMemberParameters WithUntilDate(DateTime untilDate)
        {
            UntilDate = untilDate;
            return this;
        }
    }
}