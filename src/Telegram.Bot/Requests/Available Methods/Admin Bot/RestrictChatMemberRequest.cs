using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Restrict a user in a supergroup. The bot must be an administrator in the supergroup for this to work and must have the appropriate admin rights. Pass True for all boolean parameters to lift restrictions from a user.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class RestrictChatMemberRequest : RequestBase<bool>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target supergroup (in the format @supergroupusername)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ChatId ChatId { get; }

        /// <summary>
        /// Unique identifier of the target user
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int UserId { get; }

        /// <summary>
        /// Date when restrictions will be lifted for the user, unix time. If user is restricted for more than 366 days or less than 30 seconds from the current time, they are considered to be restricted forever.
        /// </summary>
        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime UntilDate { get; set; }

        /// <summary>
        /// Pass True, if the user can send text messages, contacts, locations and venues
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? CanSendMessages { get; set; }

        /// <summary>
        /// Pass True, if the user can send audios, documents, photos, videos, video notes and voice notes, implies <see cref="CanSendMessages"/>
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? CanSendMediaMessages { get; set; }

        /// <summary>
        /// Pass True, if the user can send animations, games, stickers and use inline bots, implies <see cref="CanSendMediaMessages"/>
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? CanSendOtherMessages { get; set; }

        /// <summary>
        /// Pass True, if the user may add web page previews to their messages, implies <see cref="CanSendMediaMessages"/>
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? CanAddWebPagePreviews { get; set; }

        /// <summary>
        /// Initializes a new request with chatId and userId
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
        /// <param name="userId">Unique identifier of the target user</param>
        public RestrictChatMemberRequest(ChatId chatId, int userId)
            : base("restrictChatMember")
        {
            ChatId = chatId;
            UserId = userId;
        }
    }
}
