using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.EditMessageCaptionAsync" /> method.
    /// </summary>
    public class EditMessageCaptionInlineParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier of the sent message
        /// </summary>
        public string InlineMessageId { get; set; }

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
        ///     Sets <see cref="InlineMessageId" /> property.
        /// </summary>
        /// <param name="inlineMessageId">Unique identifier of the sent message</param>
        public EditMessageCaptionInlineParameters WithInlineMessageId(string inlineMessageId)
        {
            InlineMessageId = inlineMessageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Caption" /> property.
        /// </summary>
        /// <param name="caption">New caption of the message</param>
        public EditMessageCaptionInlineParameters WithCaption(string caption)
        {
            Caption = caption;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyMarkup" /> property.
        /// </summary>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        public EditMessageCaptionInlineParameters WithReplyMarkup(InlineKeyboardMarkup replyMarkup)
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
        public EditMessageCaptionInlineParameters WithParseMode(ParseMode parseMode)
        {
            ParseMode = parseMode;
            return this;
        }
    }
}