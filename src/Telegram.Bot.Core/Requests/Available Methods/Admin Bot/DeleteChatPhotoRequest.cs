using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Delete a chat photo. Photos can't be changed for private chats. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
    /// </summary>
    public sealed class DeleteChatPhotoRequest : ChatIdRequestBase<bool>
    {
        /// <summary>
        /// Initializes a new request with <see cref="ChatId"/> set to 0
        /// </summary>
        public DeleteChatPhotoRequest() : this(0)
        {
        }

        /// <summary>
        /// Initializes a new request with chat_id and photo
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        public DeleteChatPhotoRequest([NotNull] ChatId chatId) : base("deleteChatPhoto")
        {
            ChatId = chatId;
        }
    }
}
