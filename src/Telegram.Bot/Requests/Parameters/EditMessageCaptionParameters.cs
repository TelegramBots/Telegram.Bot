using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.EditMessageCaptionAsync" /> method.
    /// </summary>
    public class EditMessageCaptionParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        ///     New caption of the message
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        ///     A JSON-serialized object for an inline keyboard.
        /// </summary>
        public InlineKeyboardMarkup ReplyMarkup { get; set; }

        /// <summary>
        ///     Send Markdown or HTML, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in the media
        ///     caption.
        /// </summary>
        public ParseMode ParseMode { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        public EditMessageCaptionParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="MessageId" /> property.
        /// </summary>
        public EditMessageCaptionParameters WithMessageId(int messageId)
        {
            MessageId = messageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Caption" /> property.
        /// </summary>
        /// <param name="caption">New caption of the message</param>
        public EditMessageCaptionParameters WithCaption(string caption)
        {
            Caption = caption;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyMarkup" /> property.
        /// </summary>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        public EditMessageCaptionParameters WithReplyMarkup(InlineKeyboardMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ParseMode" /> property.
        /// </summary>
        /// <param name="parseMode">
        ///     Send Markdown or HTML, if you want Telegram apps to show bold, italic, fixed-width text or
        ///     inline URLs in the media caption.
        /// </param>
        public EditMessageCaptionParameters WithParseMode(ParseMode parseMode)
        {
            ParseMode = parseMode;
            return this;
        }
    }
}
