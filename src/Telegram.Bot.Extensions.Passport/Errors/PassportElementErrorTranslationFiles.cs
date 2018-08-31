using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// Represents an issue with the translated version of a document. The error is considered resolved when a
    /// file with the document translation change.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PassportElementErrorTranslationFiles : PassportElementError
    {
        /// <summary>
        /// List of base64-encoded file hashes
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public IEnumerable<string> FileHashes { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="PassportElementErrorTranslationFiles"/> with required parameters
        /// </summary>
        /// <param name="type">
        /// Type of element of the user's Telegram Passport which has the issue, one of "passport", "driver_license",
        /// "identity_card", "internal_passport", "utility_bill", "bank_statement", "rental_agreement",
        /// "passport_registration", "temporary_registration"
        /// </param>
        /// <param name="fileHashes">List of base64-encoded file hashes</param>
        /// <param name="message">Error message</param>
        public PassportElementErrorTranslationFiles(
            string type,
            IEnumerable<string> fileHashes,
            string message
        )
            : base("translation_files", type, message)
        {
            FileHashes = fileHashes;
        }
    }
}
