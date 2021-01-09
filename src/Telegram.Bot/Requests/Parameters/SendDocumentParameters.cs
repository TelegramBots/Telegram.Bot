using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SendDocumentAsync" /> method.
    /// </summary>
    public class SendDocumentParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     File to send.
        /// </summary>
        public InputOnlineFile Document { get; set; }

        /// <summary>
        ///     Document caption
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        ///     Change, if you want Telegram apps to show bold, italic, fixed-width text or inline
        ///     URLs in your bot's message.
        /// </summary>
        public ParseMode ParseMode { get; set; }

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
        public SendDocumentParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Document" /> property.
        /// </summary>
        /// <param name="document">File to send.</param>
        public SendDocumentParameters WithDocument(InputOnlineFile document)
        {
            Document = document;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Caption" /> property.
        /// </summary>
        /// <param name="caption">Document caption</param>
        public SendDocumentParameters WithCaption(string caption)
        {
            Caption = caption;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ParseMode" /> property.
        /// </summary>
        /// <param name="parseMode">
        ///     Change, if you want Telegram apps to show bold, italic, fixed-width text or inline
        ///     URLs in your bot's message.
        /// </param>
        public SendDocumentParameters WithParseMode(ParseMode parseMode)
        {
            ParseMode = parseMode;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableNotification" /> property.
        /// </summary>
        /// <param name="disableNotification">
        ///     Sends the message silently. iOS users will not receive a notification,
        ///     Android users will receive a notification with no sound.
        /// </param>
        public SendDocumentParameters WithDisableNotification(bool disableNotification)
        {
            DisableNotification = disableNotification;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyToMessageId" /> property.
        /// </summary>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        public SendDocumentParameters WithReplyToMessageId(int replyToMessageId)
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
        public SendDocumentParameters WithReplyMarkup(IReplyMarkup replyMarkup)
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
        public SendDocumentParameters WithThumb(InputMedia thumb)
        {
            Thumb = thumb;
            return this;
        }
    }
}
