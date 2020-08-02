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
        public SecureData SecureData { get; set; } = default!;

        /// <summary>
        /// Bot-specified nonce. Make sure that the payload is the same as was passed in the
        /// request.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Nonce { get; set; }
    }
}
