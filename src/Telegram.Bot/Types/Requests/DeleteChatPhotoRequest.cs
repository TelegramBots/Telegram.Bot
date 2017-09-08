using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to delete a chat's photo
    /// </summary>
    public class DeleteChatPhotoRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteChatPhotoRequest"/> class
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        public DeleteChatPhotoRequest(ChatId chatId) : base("deleteChatPhoto",
            new Dictionary<string, object> { { "chat_id", chatId } })
        {

        }
    }
}
