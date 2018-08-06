using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// This object represents the credentials required to decrypt encrypted value. All fields are optional and depend on the type of field.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SecureValue
    {
        /// <summary>
        /// Optional. Credentials for encrypted Telegram Passport data. Available for “personal_details”, “passport”, “driver_license”, “identity_card”, “identity_passport” and “address” types.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DataCredentials Data;

        /// <summary>
        /// Optional. Credentials for encrypted document's front side. Available for “passport”, “driver_license”, “identity_card” and “internal_passport”.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public FileCredentials FrontSide;

        /// <summary>
        /// Optional. Credentials for encrypted document's reverse side. Available for “driver_license” and “identity_card”.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public FileCredentials ReverseSide;

        /// <summary>
        /// Optional. Credentials for encrypted selfie of the user with a document. Can be available for “passport”, “driver_license”, “identity_card” and “internal_passport”.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public FileCredentials Selfie;

        /// <summary>
        /// Optional. Credentials for encrypted files. Available for “utility_bill”, “bank_statement”, “rental_agreement”, “passport_registration” and “temporary_registration” types.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public FileCredentials[] Files;
    }
}
