using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to set a chat's description
    /// </summary>
    public class SetChatDescriptionRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetChatDescriptionRequest"/> class
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="description">New chat description, 0-255 characters. Defaults to an empty string, which would clear the description.</param>
        public SetChatDescriptionRequest(ChatId chatId, string description = "") : base("setChatDescription", 
            new Dictionary<string, object>()
            {
                { "chat_id", chatId }
            })
        {
            if (!string.IsNullOrWhiteSpace(description))
                Parameters.Add("description", description);
        }
    }
}
