using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// This object represents personal details.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PersonalDetails : IDecryptedValue
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
        /// Optional. Middle Name
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string MiddleName { get; set; }

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

        /// <summary>
        /// First Name in the language of the user's country of residence
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string FirstNameNative { get; set; }

        /// <summary>
        /// Last Name in the language of the user's country of residence
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string LastNameNative { get; set; }

        /// <summary>
        /// Optional. Middle Name in the language of the user's country of residence
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string MiddleNameNative { get; set; }

        /// <summary>
        /// Date of birth
        /// </summary>
        public DateTime Birthdate =>
            DateTime.ParseExact(BirthDate, "dd.MM.yyyy", null, DateTimeStyles.None);
    }
}
