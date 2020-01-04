using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Export an invite link to a supergroup or a channel. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
    /// </summary>
    public sealed class ExportChatInviteLinkRequest : ChatIdRequestBase<string>
    {
        /// <summary>
        /// Initializes a new request with <see cref="ChatId"/> set to 0
        /// </summary>
        public ExportChatInviteLinkRequest() : this(0)
        {
        }

        /// <summary>
        /// Initializes a new request with specified <see cref="ChatId"/>
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel.</param>
        public ExportChatInviteLinkRequest([NotNull] ChatId chatId)
            : base("exportChatInviteLink")
        {
            ChatId = chatId;
        }
    }
}
