using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.EditMessageTextAsync" /> method.
    /// </summary>
    public class EditMessageTextInlineParameters : ParametersBase
    {
        /// <summary>
        ///     Identifier of the inline message
        /// </summary>
        public string InlineMessageId { get; set; }

        /// <summary>
        ///     New text of the message
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     Send Markdown or HTML, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your
        ///     bot's message.
        /// </summary>
        public ParseMode ParseMode { get; set; }

        /// <summary>
        ///     Disables link previews for links in this message
        /// </summary>
        public bool DisableWebPagePreview { get; set; }

        /// <summary>
        ///     A JSON-serialized object for an inline keyboard.
        /// </summary>
        public InlineKeyboardMarkup ReplyMarkup { get; set; }

        /// <summary>
        ///     Sets <see cref="InlineMessageId" /> property.
        /// </summary>
        /// <param name="inlineMessageId">Identifier of the inline message</param>
        public EditMessageTextInlineParameters WithInlineMessageId(string inlineMessageId)
        {
            InlineMessageId = inlineMessageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Text" /> property.
        /// </summary>
        /// <param name="text">New text of the message</param>
        public EditMessageTextInlineParameters WithText(string text)
        {
            Text = text;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ParseMode" /> property.
        /// </summary>
        /// <param name="parseMode">
        ///     Send Markdown or HTML, if you want Telegram apps to show bold, italic, fixed-width text or
        ///     inline URLs in your bot's message.
        /// </param>
        public EditMessageTextInlineParameters WithParseMode(ParseMode parseMode)
        {
            ParseMode = parseMode;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableWebPagePreview" /> property.
        /// </summary>
        /// <param name="disableWebPagePreview">Disables link previews for links in this message</param>
        public EditMessageTextInlineParameters WithDisableWebPagePreview(bool disableWebPagePreview)
        {
            DisableWebPagePreview = disableWebPagePreview;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyMarkup" /> property.
        /// </summary>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        public EditMessageTextInlineParameters WithReplyMarkup(InlineKeyboardMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }
    }
}