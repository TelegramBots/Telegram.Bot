using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SendStickerAsync" /> method.
    /// </summary>
    public class SendStickerParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Sticker to send.
        /// </summary>
        public InputOnlineFile Sticker { get; set; }

        /// <summary>
        ///     Sends the message silently. iOS users will not receive a notification, Android users will receive a notification
        ///     with no sound.
        /// </summary>
        public bool DisableNotification { get; set; }

        /// <summary>
        ///     If the message is a reply, ID of the original message
        /// </summary>
        public int ReplyToMessageId { get; set; }

        /// <summary>
        ///     Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard
        ///     or to force a reply from the user.
        /// </summary>
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId"></param>
        public SendStickerParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Sticker" /> property.
        /// </summary>
        /// <param name="sticker">Sticker to send.</param>
        public SendStickerParameters WithSticker(InputOnlineFile sticker)
        {
            Sticker = sticker;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableNotification" /> property.
        /// </summary>
        /// <param name="disableNotification">
        ///     Sends the message silently. iOS users will not receive a notification, Android users
        ///     will receive a notification with no sound.
        /// </param>
        public SendStickerParameters WithDisableNotification(bool disableNotification)
        {
            DisableNotification = disableNotification;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyToMessageId" /> property.
        /// </summary>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        public SendStickerParameters WithReplyToMessageId(int replyToMessageId)
        {
            ReplyToMessageId = replyToMessageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyMarkup" /> property.
        /// </summary>
        /// <param name="replyMarkup">
        ///     Additional interface options. A JSON-serialized object for a custom reply keyboard,
        ///     instructions to hide keyboard or to force a reply from the user.
        /// </param>
        public SendStickerParameters WithReplyMarkup(IReplyMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }
    }
}
