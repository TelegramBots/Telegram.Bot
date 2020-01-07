using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Use this method to set a custom title for an administrator in a supergroup promoted by the bot. Returns True on success.
    /// </summary>
    [DataContract]
    public class SetChatAdministratorCustomTitleRequest : ChatIdRequestBase<bool>
    {
        /// <summary>
        /// Unique identifier of the target user
        /// </summary>
        [DataMember(IsRequired = true)]
        public int UserId { get; set; }

        /// <summary>
        /// New custom title for the administrator; 0-16 characters, emoji are not allowed
        /// </summary>
        [DataMember(IsRequired = true)]
        [NotNull]
        public string CustomTitle { get; }

        /// <summary>
        /// Initializes a new request with chatId and new title
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="userId">Unique identifier of the target user</param>
        /// <param name="customTitle">New custom title for the administrator; 0-16 characters, emoji are not allowed</param>
        public SetChatAdministratorCustomTitleRequest([NotNull] ChatId chatId, int userId, [NotNull] string customTitle)
            : base("setChatAdministratorCustomTitle")
        {
            ChatId = chatId;
            UserId = userId;
            CustomTitle = customTitle;
        }
    }
}
