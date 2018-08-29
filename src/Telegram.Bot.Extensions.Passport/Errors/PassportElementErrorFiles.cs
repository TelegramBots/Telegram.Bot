using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// Represents an issue with a list of scans. The error is considered resolved when the list of files containing the scans changes.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PassportElementErrorFiles : PassportElementError
    {
        /// <summary>
        /// Base64-encoded hash of the file with the front side of the document.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public IEnumerable<string> FileHashes { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type">The section of the user's Telegram Passport which has the issue, one of “utility_bill”, “bank_statement”, “rental_agreement”, “passport_registration”, “temporary_registration”</param>
        /// <param name="fileHashes">List of base64-encoded file hashes</param>
        /// <param name="message"></param>
        public PassportElementErrorFiles(
            string type,
            IEnumerable<string> fileHashes,
            string message)
            : base(type, "files", message)
        {
            FileHashes = fileHashes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHashes">List of base64-encoded file hashes</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorFiles WithUtilityBill(IEnumerable<string> fileHashes, string message)
            =>
                new PassportElementErrorFiles(
                    PassportEnums.Scope.UtilityBill,
                    fileHashes,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHashes">List of base64-encoded file hashes</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorFiles WithBankStatement(IEnumerable<string> fileHashes, string message)
            =>
                new PassportElementErrorFiles(
                    PassportEnums.Scope.BankStatement,
                    fileHashes,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHashes">List of base64-encoded file hashes</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorFiles WithRentalAgreement(IEnumerable<string> fileHashes, string message)
            =>
                new PassportElementErrorFiles(
                    PassportEnums.Scope.RentalAgreement,
                    fileHashes,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHashes">List of base64-encoded file hashes</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorFiles WithPassportRegistration(IEnumerable<string> fileHashes, string message)
            =>
                new PassportElementErrorFiles(
                    PassportEnums.Scope.PassportRegistration,
                    fileHashes,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHashes">List of base64-encoded file hashes</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorFiles WithTemporaryRegistration(IEnumerable<string> fileHashes, string message)
            =>
                new PassportElementErrorFiles(
                    PassportEnums.Scope.TemporaryRegistration,
                    fileHashes,
                    message
                );
    }
}
