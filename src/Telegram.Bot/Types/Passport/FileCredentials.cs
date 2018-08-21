using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// These credentials can be used to decrypt encrypted files from the front_side, reverse_side, selfie and files fields in EncryptedPassportData.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class FileCredentials
    {
        /// <summary>
        /// Checksum of encrypted file
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string FileHash { get; set; }

        /// <summary>
        /// Secret of encrypted file
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Secret { get; set; }
    }
}
