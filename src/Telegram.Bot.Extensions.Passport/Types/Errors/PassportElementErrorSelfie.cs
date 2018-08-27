using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// Represents an issue with the selfie with a document. The error is considered resolved when the file with the selfie changes.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PassportElementErrorSelfie : PassportElementError
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
        /// <param name="fileHash">Base64-encoded hash of the file with the selfie</param>
        /// <param name="message">Error message</param>
        public PassportElementErrorSelfie(
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
        public static PassportElementErrorSelfie WithInternalPassport(string fileHash, string message)
            =>
                new PassportElementErrorSelfie(
                    PassportEnums.ElementType.InternalPassport,
                    fileHash,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHash">Base64-encoded hash of the file with the front side of the document</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorSelfie WithIdentityCard(string fileHash, string message)
            =>
                new PassportElementErrorSelfie(
                    PassportEnums.ElementType.IdentityCard,
                    fileHash,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHash">Base64-encoded hash of the file with the front side of the document</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorSelfie WithDriverLicense(string fileHash, string message)
            =>
                new PassportElementErrorSelfie(
                    PassportEnums.ElementType.DriverLicense,
                    fileHash,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHash">Base64-encoded hash of the file with the front side of the document</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorSelfie WithPassport(string fileHash, string message)
            =>
                new PassportElementErrorSelfie(
                    PassportEnums.ElementType.Passport,
                    fileHash,
                    message
                );
    }
}
