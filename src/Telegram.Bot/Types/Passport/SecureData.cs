using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// This object represents the credentials required to decrypt encrypted data. All fields are optional and depend on fields that were requested.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SecureData
    {
        /// <summary>
        /// Optional. Credentials for encrypted personal details
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SecureValue PersonalDetails { get; set; }

        /// <summary>
        /// Optional. Credentials for encrypted passport
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SecureValue Passport { get; set; }

        /// <summary>
        /// Optional. Credentials for encrypted internal passport
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SecureValue InternalPassport { get; set; }

        /// <summary>
        /// Optional. Credentials for encrypted driver license
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SecureValue DriverLicense { get; set; }

        /// <summary>
        /// Optional. Credentials for encrypted ID card
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SecureValue IdentityCard { get; set; }

        /// <summary>
        /// Optional. Credentials for encrypted residential address
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SecureValue Address { get; set; }

        /// <summary>
        /// Optional. Credentials for encrypted utility bill
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SecureValue UtilityBill { get; set; }

        /// <summary>
        /// Optional. Credentials for encrypted bank statement
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SecureValue BankStatement { get; set; }

        /// <summary>
        /// Optional. Credentials for encrypted rental agreement
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SecureValue RentalAgreement { get; set; }

        /// <summary>
        /// Optional. Credentials for encrypted registration from internal passport
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SecureValue PassportRegistration { get; set; }

        /// <summary>
        /// Optional. Credentials for encrypted temporary registration
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SecureValue TemporaryRegistration { get; set; }
    }
}
