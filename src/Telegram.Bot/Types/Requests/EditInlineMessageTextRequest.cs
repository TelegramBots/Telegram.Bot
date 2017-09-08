using System.Collections.Generic;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to edit an inline messages text
    /// </summary>
    public class EditInlineMessageTextRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditInlineMessageTextRequest"/> class
        /// </summary>
        /// <param name="inlineMessageId">Identifier of the inline message</param>
        /// <param name="text">New text of the message</param>
        /// <param name="parseMode">Send Markdown or HTML, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your bot's message.</param>
        /// <param name="disableWebPagePreview">Disables link previews for links in this message</param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        public EditInlineMessageTextRequest(string inlineMessageId, string text,
            ParseMode parseMode = ParseMode.Default,
            bool disableWebPagePreview = false,
            IReplyMarkup replyMarkup = null) : base("editMessageText",
                new Dictionary<string, object>
                {
                    {"inline_message_id", inlineMessageId},
                    {"text", text},
                    {"disable_web_page_preview", disableWebPagePreview},
                })
        {
            if (replyMarkup != null)
                Parameters.Add("reply_markup", replyMarkup);

            if (parseMode != ParseMode.Default)
                Parameters.Add("parse_mode", parseMode.ToModeString());
        }
    }
}
