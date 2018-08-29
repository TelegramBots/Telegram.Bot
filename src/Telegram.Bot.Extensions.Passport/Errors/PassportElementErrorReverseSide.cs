using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// Represents an issue with the reverse side of a document. The error is considered resolved when the file with reverse side of the document changes.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PassportElementErrorReverseSide : PassportElementError
    {
        /// <summary>
        /// Base64-encoded hash of the file with the front side of the document.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string FileHash { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type">The section of the user's Telegram Passport which has the issue, one of “driver_license”, “identity_card”</param>
        /// <param name="fileHash">Base64-encoded hash of the file with the reverse side of the document</param>
        /// <param name="message">Error message</param>
        public PassportElementErrorReverseSide(
            string type,
            string fileHash,
            string message)
            : base(type, "reverse_side", message)
        {
            FileHash = fileHash;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHash">Base64-encoded hash of the file with the front side of the document</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorReverseSide WithIdentityCard(string fileHash, string message)
            =>
                new PassportElementErrorReverseSide(
                    PassportEnums.Scope.IdentityCard,
                    fileHash,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHash">Base64-encoded hash of the file with the front side of the document</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorReverseSide WithDriverLicense(string fileHash, string message)
            =>
                new PassportElementErrorReverseSide(
                    PassportEnums.Scope.DriverLicense,
                    fileHash,
                    message
                );
    }
}
