using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Delete a group sticker set from a supergroup
    /// </summary>
    public sealed class DeleteChatStickerSetRequest : ChatIdRequestBase<bool>
    {
        /// <summary>
        /// Initializes a new request with specified <see cref="ChatId"/>
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        public DeleteChatStickerSetRequest([NotNull] ChatId chatId) : base("deleteChatStickerSet")
        {
            ChatId = chatId;
        }
    }
}
