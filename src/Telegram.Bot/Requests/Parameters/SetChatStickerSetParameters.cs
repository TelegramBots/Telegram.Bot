using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SetChatStickerSetAsync" /> method.
    /// </summary>
    public class SetChatStickerSetParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the target chat or username of the target supergroup (in the format @supergroupusername)
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Name of the sticker set to be set as the group sticker set
        /// </summary>
        public string StickerSetName { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId">
        ///     Unique identifier for the target chat or username of the target supergroup (in the format
        ///     @supergroupusername)
        /// </param>
        public SetChatStickerSetParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="StickerSetName" /> property.
        /// </summary>
        /// <param name="stickerSetName">Name of the sticker set to be set as the group sticker set</param>
        public SetChatStickerSetParameters WithStickerSetName(string stickerSetName)
        {
            StickerSetName = stickerSetName;
            return this;
        }
    }
}