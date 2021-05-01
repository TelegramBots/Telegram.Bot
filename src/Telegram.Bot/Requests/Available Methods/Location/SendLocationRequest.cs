using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Send point on the map
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SendLocationRequest : RequestBase<Message>,
                                       INotifiableMessage,
                                       IReplyMessage,
                                       IReplyMarkupMessage<IReplyMarkup>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ChatId ChatId { get; }

        /// <summary>
        /// Latitude of the location
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public float Latitude { get; }

        /// <summary>
        /// Longitude of the location
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public float Longitude { get; }

        /// <summary>
        /// Period in seconds for which the location will be updated, should be between 60 and 86400
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int LivePeriod { get; set; }

        /// <summary>
        /// For live locations, a direction in which the user is moving, in degrees. Must be between 1 and 360 if specified.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Heading { get; set; }

        /// <summary>
        /// For live locations, a maximum distance for proximity alerts about approaching another chat member, in meters. Must be between 1 and 100000 if specified.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ProximityAlertRadius { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ReplyToMessageId { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool AllowSendingWithoutReply { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request with chatId and location
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="latitude">Latitude of the location</param>
        /// <param name="longitude">Longitude of the location</param>
        public SendLocationRequest(ChatId chatId, float latitude, float longitude)
            : base("sendLocation")
        {
            ChatId = chatId;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
