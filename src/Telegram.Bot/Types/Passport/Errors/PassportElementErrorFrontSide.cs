using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// Represents an issue with the front side of a document. The error is considered resolved when the file with the front side of the document changes.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PassportElementErrorFrontSide : PassportElementError
    {
        /// <summary>
        /// Base64-encoded hash of the file with the front side of the document.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string FileHash { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type">The section of the user's Telegram Passport which has the issue, one of “passport”, “driver_license”, “identity_card”, “internal_passport”</param>
        /// <param name="fileHash">Base64-encoded hash of the file with the front side of the document</param>
        /// <param name="message">Error message</param>
        public PassportElementErrorFrontSide(
            string type,
            string fileHash,
            string message)
            : base(type, "front_side", message)
        {
            FileHash = fileHash;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHash">Base64-encoded hash of the file with the front side of the document</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorFrontSide WithInternalPassport(string fileHash, string message)
            =>
                new PassportElementErrorFrontSide(
                    PassportElementType.InternalPassport,
                    fileHash,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHash">Base64-encoded hash of the file with the front side of the document</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorFrontSide WithIdentityCard(string fileHash, string message)
            =>
                new PassportElementErrorFrontSide(
                    PassportElementType.IdentityCard,
                    fileHash,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHash">Base64-encoded hash of the file with the front side of the document</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorFrontSide WithDriverLicense(string fileHash, string message)
            =>
                new PassportElementErrorFrontSide(
                    PassportElementType.DriverLicense,
                    fileHash,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHash">Base64-encoded hash of the file with the front side of the document</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorFrontSide WithPassport(string fileHash, string message)
            =>
                new PassportElementErrorFrontSide(
                    PassportElementType.Passport,
                    fileHash,
                    message
                );
    }
}
