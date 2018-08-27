using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// This object represents personal details.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PersonalDetails
        : IDecryptedData
    {
        /// <summary>
        /// First Name
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string FirstName;

        /// <summary>
        /// Last Name
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string LastName;

        /// <summary>
        /// Date of birth in DD.MM.YYYY format
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string BirthDate;

        /// <summary>
        /// Gender, male or female
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Gender;

        /// <summary>
        /// Citizenship (ISO 3166-1 alpha-2 country code)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string CountryCode;

        /// <summary>
        /// Country of residence (ISO 3166-1 alpha-2 country code)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string ResidenceCountryCode;
    }
}
