using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Use this method to set a custom title for an administrator in a supergroup promoted by the bot. Returns True on success.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SetChatAdministratorCustomTitleRequest : RequestBase<bool>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ChatId ChatId { get; }

        /// <summary>
        /// Unique identifier of the target user
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public long UserId { get; }

        /// <summary>
        /// New custom title for the administrator; 0-16 characters, emoji are not allowed
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string CustomTitle { get; }

        /// <summary>
        /// Initializes a new request with chatId, userId and customTitle
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
        /// <param name="userId">Unique identifier of the target user</param>
        /// <param name="customTitle">New custom title for the administrator; 0-16 characters, emoji are not allowed</param>
        public SetChatAdministratorCustomTitleRequest(ChatId chatId, long userId, string customTitle)
            : base("setChatAdministratorCustomTitle")
        {
            ChatId = chatId;
            UserId = userId;
            CustomTitle = customTitle;
        }
    }
}
