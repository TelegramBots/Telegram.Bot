using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents the content of a media message to be sent
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public abstract class InputMediaBase
    {
        /// <summary>
        /// Type of the media
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Type { get; protected set; }

        /// <summary>
        /// Media to send
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public InputMedia Media { get; set; }

        /// <summary>
        /// Optional. Caption of the photo to be sent, 0-200 characters
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }
    }
}
