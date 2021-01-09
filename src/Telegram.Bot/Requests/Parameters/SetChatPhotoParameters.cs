using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SetChatPhotoAsync" /> method.
    /// </summary>
    public class SetChatPhotoParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the target chat or username of the target channel
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     The new profile picture for the chat.
        /// </summary>
        public InputFileStream Photo { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        public SetChatPhotoParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Photo" /> property.
        /// </summary>
        /// <param name="photo">The new profile picture for the chat.</param>
        public SetChatPhotoParameters WithPhoto(InputFileStream photo)
        {
            Photo = photo;
            return this;
        }
    }
}