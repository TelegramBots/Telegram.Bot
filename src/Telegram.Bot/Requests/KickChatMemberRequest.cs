using System;
using Newtonsoft.Json;
using Telegram.Bot.Converters;
using Telegram.Bot.Types;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Kick a user from a group, a supergroup or a channel
    /// </summary>
    public class KickChatMemberRequest : RequestBase<bool>
    {
        /// <summary>
        /// Unique identifier for the target group or username of the target supergroup or channel
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// Unique identifier of the target user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Date when the user will be unbanned, unix time. If user is banned for more than 366 days or less than 30 seconds from the current time they are considered to be banned forever.
        /// </summary>
        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime UntilDate { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public KickChatMemberRequest()
            : base("kickChatMember")
        { }

        /// <summary>
        /// Initializes a new request with chatId and userId
        /// </summary>
        /// <param name="chatId">Unique identifier for the target group or username of the target supergroup or channel</param>
        /// <param name="userId">Unique identifier of the target user</param>
        public KickChatMemberRequest(ChatId chatId, int userId)
            : this()
        {
            ChatId = chatId;
            UserId = userId;
        }
    }
}
