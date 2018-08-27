using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// Credentials is a JSON-serialized object.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Credentials
    {
        /// <summary>
        /// Credentials for encrypted data
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public SecureData SecureData;

        /// <summary>
        /// Bot-specified payload
        /// <para>Make sure that the payload is the same as was passed in the request.</para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Payload;
    }
}
