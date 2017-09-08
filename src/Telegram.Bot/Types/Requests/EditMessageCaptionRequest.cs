using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to edit a message caption
    /// </summary>
    public class EditMessageCaptionRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditMessageCaptionRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="messageId">Unique identifier of the sent message</param>
        /// <param name="caption">New caption of the message</param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        public EditMessageCaptionRequest(ChatId chatId, int messageId, string caption,
            IReplyMarkup replyMarkup = null) : base("editMessageCaption",
                new Dictionary<string, object>
                {
                    {"chat_id", chatId},
                    {"message_id", messageId},
                    {"caption", caption},
                })
        {
            if (replyMarkup != null)
                Parameters.Add("reply_markup", replyMarkup);
        }
    }
}
