using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// Credentials is a JSON-serialized object.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Credentials : IDecryptedValue
    {
        /// <summary>
        /// Credentials for encrypted data
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public SecureData SecureData;

        /// <summary>
        /// Bot-specified payload. Make sure that the payload is the same as was passed in the request.
        /// </summary>
        [JsonProperty]
        public string Payload;

        /// <summary>
        /// Bot-specified nonce. Make sure that the payload is the same as was passed in the request.
        /// </summary>
        [JsonProperty]
        public string Nonce;
    }
}
