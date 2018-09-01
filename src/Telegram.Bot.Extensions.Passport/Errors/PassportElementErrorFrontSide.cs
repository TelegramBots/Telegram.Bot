using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// Represents an issue with the front side of a document. The error is considered resolved when the file with
    /// the front side of the document changes.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PassportElementErrorFrontSide : PassportElementError
    {
        /// <summary>
        /// Base64-encoded hash of the file with the front side of the document
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string FileHash { get; }

        /// <summary>
        /// Initialize a new instance of <see cref="PassportElementErrorFrontSide"/> with required parameters
        /// </summary>
        /// <param name="type">
        /// The section of the user's Telegram Passport which has the issue, one of "passport", "driver_license",
        /// "identity_card", "internal_passport"
        /// </param>
        /// <param name="fileHash">Base64-encoded hash of the file with the front side of the document</param>
        /// <param name="message">Error message</param>
        /// <exception cref="ArgumentNullException">if any argument is null</exception>
        public PassportElementErrorFrontSide(
            string type,
            string fileHash,
            string message
        )
            : base("front_side", type, message)
        {
            FileHash = fileHash ?? throw new ArgumentNullException(nameof(fileHash));
        }
    }
}
