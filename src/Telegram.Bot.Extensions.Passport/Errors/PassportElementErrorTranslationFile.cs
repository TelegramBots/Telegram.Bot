using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// Represents an issue with one of the files that constitute the translation of a document. The error is
    /// considered resolved when the file changes.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PassportElementErrorTranslationFile : PassportElementError
    {
        /// <summary>
        /// Base64-encoded file hash
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string FileHash { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="PassportElementErrorTranslationFile"/> with required parameters
        /// </summary>
        /// <param name="type">
        /// Type of element of the user's Telegram Passport which has the issue, one of "passport",
        /// "driver_license", "identity_card", "internal_passport", "utility_bill", "bank_statement",
        /// "rental_agreement", "passport_registration", "temporary_registration"
        /// </param>
        /// <param name="fileHash">Base64-encoded file hash</param>
        /// <param name="message">Error message</param>
        public PassportElementErrorTranslationFile(
            string type,
            string fileHash,
            string message
        )
            : base("translation_file", type, message)
        {
            FileHash = fileHash;
        }
    }
}
