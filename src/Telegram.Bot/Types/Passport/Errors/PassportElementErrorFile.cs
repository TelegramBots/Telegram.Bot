﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// Represents an issue with a document scan. The error is considered resolved when the file with the document scan changes.
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
        ///
        /// </summary>
        /// <param name="type">The section of the user's Telegram Passport which has the issue, one of “utility_bill”, “bank_statement”, “rental_agreement”, “passport_registration”, “temporary_registration”</param>
        /// <param name="fileHash">Base64-encoded file hash</param>
        /// <param name="message">Error message</param>
        public PassportElementErrorFile(
            string type,
            string fileHash,
            string message)
            : base(type, "file", message)
        {
            FileHash = fileHash;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHash">Base64-encoded hash of the file with the front side of the document</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorFile WithUtilityBill(string fileHash, string message)
            =>
                new PassportElementErrorFile(
                    PassportElementType.UtilityBill,
                    fileHash,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHash">Base64-encoded hash of the file with the front side of the document</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorFile WithBankStatement(string fileHash, string message)
            =>
                new PassportElementErrorFile(
                    PassportElementType.BankStatement,
                    fileHash,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHash">Base64-encoded hash of the file with the front side of the document</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorFile WithRentalAgreement(string fileHash, string message)
            =>
                new PassportElementErrorFile(
                    PassportElementType.RentalAgreement,
                    fileHash,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHash">Base64-encoded hash of the file with the front side of the document</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorFile WithPassportRegistration(string fileHash, string message)
            =>
                new PassportElementErrorFile(
                    PassportElementType.PassportRegistration,
                    fileHash,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileHash">Base64-encoded hash of the file with the front side of the document</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorFile WithTemporaryRegistration(string fileHash, string message)
            =>
                new PassportElementErrorFile(
                    PassportElementType.TemporaryRegistration,
                    fileHash,
                    message
                );
    }
}
