using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// Represents an issue with the front side of a document. The error is considered resolved when the file with
    /// the reverse side of the document changes.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PassportElementErrorReverseSide : PassportElementError
    {
        /// <summary>
        /// Base64-encoded hash of the file with the reverse side of the document
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string FileHash { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="PassportElementErrorReverseSide"/> with required parameters
        /// </summary>
        /// <param name="type">
        /// The section of the user's Telegram Passport which has the issue, one of "driver_license", "identity_card"
        /// </param>
        /// <param name="fileHash">Base64-encoded hash of the file with the reverse side of the document</param>
        /// <param name="message">Error message</param>
        /// <exception cref="ArgumentNullException">if any argument is null</exception>
        public PassportElementErrorReverseSide(
            string type,
            string fileHash,
            string message
        )
            : base("reverse_side", type, message)
        {
            FileHash = fileHash ?? throw new ArgumentNullException(nameof(fileHash));
        }
    }
}
