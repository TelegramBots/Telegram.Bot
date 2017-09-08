using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to set a chat's title
    /// </summary>
    public class SetChatTitleRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetChatTitleRequest"/> class
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="title">New chat title, 1-255 characters</param>
        public SetChatTitleRequest(ChatId chatId, string title) : base("setChatTitle",
            new Dictionary<string, object>()
            {
                { "chat_id", chatId },
                { "title", title }
            })
        {

        }
    }
}
