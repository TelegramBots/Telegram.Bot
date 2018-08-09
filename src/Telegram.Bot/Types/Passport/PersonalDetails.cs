using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// This object represents personal details.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PersonalDetails
        #if ENABLE_CRYPTOGRAPHY
        : IDecryptedData
        #endif
    {
        /// <summary>
        /// First Name
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string LastName { get; set; }

        /// <summary>
        /// Date of birth in DD.MM.YYYY format
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string BirthDate { get; set; }

        /// <summary>
        /// Gender, male or female
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Gender { get; set; }

        /// <summary>
        /// Citizenship (ISO 3166-1 alpha-2 country code)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string CountryCode { get; set; }

        /// <summary>
        /// Country of residence (ISO 3166-1 alpha-2 country code)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string ResidenceCountryCode { get; set; }
    }
}
