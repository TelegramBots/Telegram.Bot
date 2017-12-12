using Telegram.Bot.Types;

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
        public ChatId ChatId { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public DeleteChatStickerSetRequest()
            : base("deleteChatStickerSet")
        { }

        /// <summary>
        /// Initializes a new request with chatId
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        public DeleteChatStickerSetRequest(ChatId chatId)
            : this()
        {
            ChatId = chatId;
        }
    }
}
