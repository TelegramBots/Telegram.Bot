using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to edit an inline message's reply markup
    /// </summary>
    public class EditInlineMessageReplyMarkupRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditInlineMessageReplyMarkupRequest"/> class
        /// </summary>
        /// <param name="inlineMessageId">Unique identifier of the sent message</param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        public EditInlineMessageReplyMarkupRequest(string inlineMessageId,
            IReplyMarkup replyMarkup = null) : base("editMessageReplyMarkup", 
                new Dictionary<string, object>
                {
                    {"inline_message_id", inlineMessageId},
                })
        {
            if (replyMarkup != null)
                Parameters.Add("reply_markup", replyMarkup);
        }
    }
}
