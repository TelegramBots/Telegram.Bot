using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to edit an inline message's caption
    /// </summary>
    public class EditInlineMessageCaptionRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditInlineMessageCaptionRequest"/> class
        /// </summary>
        /// <param name="inlineMessageId">Unique identifier of the sent message</param>
        /// <param name="caption">New caption of the message</param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        public EditInlineMessageCaptionRequest(string inlineMessageId, string caption,
            IReplyMarkup replyMarkup = null) : base("editMessageCaption",
                new Dictionary<string, object>
                {
                    {"inline_message_id", inlineMessageId},
                    {"caption", caption},
                })
        {
            if (replyMarkup != null)
                Parameters.Add("reply_markup", replyMarkup);
        }
    }
}
