using Telegram.Bot.Types;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Set a new group sticker set for a supergroup
    /// </summary>
    public class SetChatStickerSetRequest : RequestBase<bool>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// Name of the sticker set to be set as the group sticker set
        /// </summary>
        public string StickerSetName { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public SetChatStickerSetRequest()
            : base("setChatStickerSet")
        { }

        /// <summary>
        /// Initializes a new request with chatId and new stickerSetName
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="stickerSetName">Name of the sticker set to be set as the group sticker set</param>
        public SetChatStickerSetRequest(ChatId chatId, string stickerSetName)
            : this()
        {
            ChatId = chatId;
            StickerSetName = stickerSetName;
        }
    }
}
