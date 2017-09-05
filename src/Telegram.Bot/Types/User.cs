using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a Telegram user or bot.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class User
    {
        /// <summary>
        /// Unique identifier for this user or bot
        /// </summary>
        /// <returns></returns>
        [JsonProperty(PropertyName = "id", Required = Required.Always)]
        public int Id { get; set; }

        /// <summary>
        /// True, if this user is a bot
        /// </summary>
        [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy), Required = Required.Always)]
        public bool IsBot { get; set; }

        /// <summary>
        /// User's or bot's first name
        /// </summary>
        [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy), Required = Required.Always)]
        public string FirstName { get; set; }

        /// <summary>
        /// Optional. User's or bot's last name
        /// </summary>
        [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy), Required = Required.Default)]
        public string LastName { get; set; }

        /// <summary>
        /// Optional. User's or bot's username
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public string Username { get; set; }

        /// <summary>
        /// Optional. IETF language tag of the user's language
        /// </summary>
        [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
        public string LanguageCode { get; set; }
    }
}
