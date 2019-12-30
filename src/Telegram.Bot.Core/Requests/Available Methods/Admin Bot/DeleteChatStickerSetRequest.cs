using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Delete a group sticker set from a supergroup
    /// </summary>
    public class DeleteChatStickerSetRequest : RequestBase<bool>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; }

        /// <summary>
        /// Initializes a new request with chatId
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        public DeleteChatStickerSetRequest(ChatId chatId, ITelegramBotJsonConverter jsonConverter)
            : base(jsonConverter, "deleteChatStickerSet")
        {
            ChatId = chatId;
        }
    }
}
