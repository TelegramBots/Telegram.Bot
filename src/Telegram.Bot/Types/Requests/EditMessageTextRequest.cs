using System.Collections.Generic;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to edit a messages text
    /// </summary>
    public class EditMessageTextRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditMessageTextRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="messageId">Unique identifier of the sent message</param>
        /// <param name="text">New text of the message</param>
        /// <param name="parseMode">Send Markdown or HTML, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your bot's message.</param>
        /// <param name="disableWebPagePreview">Disables link previews for links in this message</param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        public EditMessageTextRequest(ChatId chatId, int messageId, string text,
            ParseMode parseMode = ParseMode.Default,
            bool disableWebPagePreview = false,
            IReplyMarkup replyMarkup = null) : base("editMessageText",
                new Dictionary<string, object>
                {
                    {"chat_id", chatId},
                    {"message_id", messageId},
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
