using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// This object represents the data of an identity document.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class IdDocumentData
        : IDecryptedData
    {
        /// <summary>
        /// Document number
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string DocumentNo;

        /// <summary>
        /// Optional. Date of expiry, in DD.MM.YYYY format
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ExpiryDate;
    }
}
