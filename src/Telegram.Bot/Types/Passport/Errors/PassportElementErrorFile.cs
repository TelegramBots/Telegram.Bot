using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// Represents an issue with a document scan. The error is considered resolved when the file with the document
    /// scan changes.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PassportElementErrorFile : PassportElementError
    {
        /// <summary>
        /// Base64-encoded hash of the file with the front side of the document.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string FileHash { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="PassportElementErrorFile"/> with required parameters
        /// </summary>
        /// <param name="type">
        /// The section of the user's Telegram Passport which has the issue, one of "utility_bill",
        /// "bank_statement", "rental_agreement", "passport_registration", "temporary_registration"
        /// </param>
        /// <param name="fileHash">Base64-encoded file hash</param>
        /// <param name="message">Error message</param>
        /// <exception cref="ArgumentNullException">if any argument is null</exception>
        public PassportElementErrorFile(
            string type,
            string fileHash,
            string message)
            : base("file", type, message)
        {
            FileHash = fileHash ?? throw new ArgumentNullException(nameof(fileHash));
        }
    }
}
