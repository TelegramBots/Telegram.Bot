using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// These credentials can be used to decrypt encrypted data from the data field in EncryptedPassportData.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class DataCredentials
    {
        /// <summary>
        /// Checksum of encrypted data
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string DataHash;

        /// <summary>
        /// Secret of encrypted data
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Secret;
    }
}
