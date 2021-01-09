using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SendVideoNoteAsync" /> method.
    /// </summary>
    public class SendVideoNoteParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Video note to send.
        /// </summary>
        public InputTelegramFile VideoNote { get; set; }

        /// <summary>
        ///     Duration of sent video in seconds
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        ///     Video width and height
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        ///     Sends the message silently. iOS users will not receive a notification,
        ///     Android users will receive a notification with no sound.
        /// </summary>
        public bool DisableNotification { get; set; }

        /// <summary>
        ///     If the message is a reply, ID of the original message
        /// </summary>
        public int ReplyToMessageId { get; set; }

        /// <summary>
        ///     Additional interface options. A JSON-serialized object for a custom reply keyboard,
        ///     instructions to hide keyboard or to force a reply from the user.
        /// </summary>
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        ///     Thumbnail of the file sent. The thumbnail should be in JPEG format and less than 200 kB
        ///     in size. A thumbnail's width and height should not exceed 90. Thumbnails can't be reused and can be only
        ///     uploaded as a new file.
        /// </summary>
        public InputMedia Thumb { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId"></param>
        public SendVideoNoteParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="VideoNote" /> property.
        /// </summary>
        /// <param name="videoNote">Video note to send.</param>
        public SendVideoNoteParameters WithVideoNote(InputTelegramFile videoNote)
        {
            VideoNote = videoNote;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Duration" /> property.
        /// </summary>
        /// <param name="duration">Duration of sent video in seconds</param>
        public SendVideoNoteParameters WithDuration(int duration)
        {
            Duration = duration;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Length" /> property.
        /// </summary>
        /// <param name="length">Video width and height</param>
        public SendVideoNoteParameters WithLength(int length)
        {
            Length = length;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableNotification" /> property.
        /// </summary>
        /// <param name="disableNotification">
        ///     Sends the message silently. iOS users will not receive a notification,
        ///     Android users will receive a notification with no sound.
        /// </param>
        public SendVideoNoteParameters WithDisableNotification(bool disableNotification)
        {
            DisableNotification = disableNotification;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyToMessageId" /> property.
        /// </summary>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        public SendVideoNoteParameters WithReplyToMessageId(int replyToMessageId)
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
        public SendVideoNoteParameters WithReplyMarkup(IReplyMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Thumb" /> property.
        /// </summary>
        /// <param name="thumb">
        ///     Thumbnail of the file sent. The thumbnail should be in JPEG format and less than 200 kB
        ///     in size. A thumbnail's width and height should not exceed 90. Thumbnails can't be reused and can be only
        ///     uploaded as a new file.
        /// </param>
        public SendVideoNoteParameters WithThumb(InputMedia thumb)
        {
            Thumb = thumb;
            return this;
        }
    }
}
