using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to unpin a message in a supergroup
    /// </summary>
    public class UnpinChatMessageRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnpinChatMessageRequest"/> class
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
        public UnpinChatMessageRequest(ChatId chatId) : base("unpinChatMessage",
            new Dictionary<string, object> { { "chat_id", chatId } })
        {

        }
    }
}
