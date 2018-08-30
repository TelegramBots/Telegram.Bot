using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// Represents an issue in an unspecified place. The error is considered resolved when new data is added.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PassportElementErrorUnspecified : PassportElementError
    {
        /// <summary>
        /// Base64-encoded element hash
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string ElementHash { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="PassportElementErrorUnspecified"/> with required parameters
        /// </summary>
        /// <param name="type">
        /// Type of element of the user's Telegram Passport which has the issue
        /// </param>
        /// <param name="elementHash">Base64-encoded element hash</param>
        /// <param name="message">Error message</param>
        public PassportElementErrorUnspecified(
            string type,
            string elementHash,
            string message
        )
            : base("unspecified", type, message)
        {
            ElementHash = elementHash;
        }
    }
}
