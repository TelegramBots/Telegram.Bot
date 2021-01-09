using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.DeleteChatStickerSetAsync" /> method.
    /// </summary>
    public class DeleteChatStickerSetParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the target chat or username of the target supergroup (in the format @supergroupusername)
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId">
        ///     Unique identifier for the target chat or username of the target supergroup (in the format
        ///     @supergroupusername)
        /// </param>
        public DeleteChatStickerSetParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }
    }
}