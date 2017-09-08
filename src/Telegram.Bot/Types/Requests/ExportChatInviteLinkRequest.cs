using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to get a chat's invite link
    /// </summary>
    public class ExportChatInviteLinkRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportChatInviteLinkRequest"/> class
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        public ExportChatInviteLinkRequest(ChatId chatId) : base("exportChatInviteLink", 
            new Dictionary<string, object> { { "chat_id", chatId } })
        {

        }
    }
}
