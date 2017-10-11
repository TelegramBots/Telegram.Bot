using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to edit a message's reply markup
    /// </summary>
    public class EditMessageReplyMarkupRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditMessageReplyMarkupRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="messageId">Unique identifier of the sent message</param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        public EditMessageReplyMarkupRequest(ChatId chatId, int messageId,
            IReplyMarkup replyMarkup = null) : base("editMessageReplyMarkup",
                new Dictionary<string, object>
                {
                    {"chat_id", chatId},
                    {"message_id", messageId},
                })
        {
            if (replyMarkup != null)
                Parameters.Add("reply_markup", replyMarkup);
        }
    }
}
