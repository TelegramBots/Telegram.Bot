using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to delete a message
    /// </summary>
    public class DeleteMessageRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteMessageRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> Unique identifier for the target chat or username of the target channel (in the format @channelusername)</param>
        /// <param name="messageId">Unique identifier of the message to delete</param>
        public DeleteMessageRequest(ChatId chatId, int messageId) : base("deleteMessage",
            new Dictionary<string, object>
            {
                {"chat_id", chatId},
                {"message_id", messageId}
            })
        {

        }
    }
}
