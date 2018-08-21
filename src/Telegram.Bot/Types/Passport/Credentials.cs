using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// Contains data required for decrypting and authenticating<see cref="EncryptedPassportElement"/> s.See the<see href="https://core.telegram.org/passport#receiving-information"> Telegram Passport Documentation</see> for a complete description of the data decryption and authentication processes.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Credentials
    {
        /// <summary>
        /// Credentials for encrypted data
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public SecureData SecureData { get; set; }

        /// <summary>
        /// Bot-specified payload
        /// <para>Make sure that the payload is the same as was passed in the request.</para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Payload { get; set; }

    }
}
