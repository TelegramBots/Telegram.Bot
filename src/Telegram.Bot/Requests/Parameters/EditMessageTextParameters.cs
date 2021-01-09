using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.EditMessageTextAsync" /> method.
    /// </summary>
    public class EditMessageTextParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// </summary>
        public int MessageId { get; set; }

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
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        public EditMessageTextParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="MessageId" /> property.
        /// </summary>
        public EditMessageTextParameters WithMessageId(int messageId)
        {
            MessageId = messageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Text" /> property.
        /// </summary>
        /// <param name="text">New text of the message</param>
        public EditMessageTextParameters WithText(string text)
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
        public EditMessageTextParameters WithParseMode(ParseMode parseMode)
        {
            ParseMode = parseMode;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableWebPagePreview" /> property.
        /// </summary>
        /// <param name="disableWebPagePreview">Disables link previews for links in this message</param>
        public EditMessageTextParameters WithDisableWebPagePreview(bool disableWebPagePreview)
        {
            DisableWebPagePreview = disableWebPagePreview;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyMarkup" /> property.
        /// </summary>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        public EditMessageTextParameters WithReplyMarkup(InlineKeyboardMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }
    }
}