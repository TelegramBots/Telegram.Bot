using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// Represents an issue in one of the data fields that was provided by the user. The error is considered resolved when the field's value changes.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PassportElementErrorDataField : PassportElementError
    {
        /// <summary>
        /// Name of the data field which has the error.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string FieldName { get; }

        /// <summary>
        /// Base64-encoded data hash.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string DataHash { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type">The section of the user's Telegram Passport which has the error, one of “personal_details”, “passport”, “driver_license”, “identity_card”, “internal_passport”, “address”</param>
        /// <param name="fieldName">Name of the data field which has the error</param>
        /// <param name="dataHash">Base64-encoded data hash</param>
        /// <param name="message">Error message</param>
        public PassportElementErrorDataField(
            string type,
            string fieldName,
            string dataHash,
            string message)
            : base(type, "data", message)
        {
            FieldName = fieldName;
            DataHash = dataHash;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fieldName">Name of the data field which has the error</param>
        /// <param name="dataHash">Base64-encoded data hash</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorDataField WithAddress(
            string fieldName,
            string dataHash,
            string message)
            =>
                new PassportElementErrorDataField(
                    PassportEnums.Scope.Address,
                    fieldName,
                    dataHash,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fieldName">Name of the data field which has the error</param>
        /// <param name="dataHash">Base64-encoded data hash</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorDataField WithInternalPassport(
            string fieldName,
            string dataHash,
            string message)
            =>
                new PassportElementErrorDataField(
                    PassportEnums.Scope.InternalPassport,
                    fieldName,
                    dataHash,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fieldName">Name of the data field which has the error</param>
        /// <param name="dataHash">Base64-encoded data hash</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorDataField WithIdentityCard(
            string fieldName,
            string dataHash,
            string message)
            =>
                new PassportElementErrorDataField(
                    PassportEnums.Scope.IdentityCard,
                    fieldName,
                    dataHash,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fieldName">Name of the data field which has the error</param>
        /// <param name="dataHash">Base64-encoded data hash</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorDataField WithDriverLicence(
            string fieldName,
            string dataHash,
            string message)
            =>
                new PassportElementErrorDataField(
                    PassportEnums.Scope.DriverLicense,
                    fieldName,
                    dataHash,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fieldName">Name of the data field which has the error</param>
        /// <param name="dataHash">Base64-encoded data hash</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorDataField WithPersonalDetails(
            string fieldName,
            string dataHash,
            string message)
            =>
                new PassportElementErrorDataField(
                    PassportEnums.Scope.PersonalDetails,
                    fieldName,
                    dataHash,
                    message
                );

        /// <summary>
        ///
        /// </summary>
        /// <param name="fieldName">Name of the data field which has the error</param>
        /// <param name="dataHash">Base64-encoded data hash</param>
        /// <param name="message">Error message</param>
        /// <returns></returns>
        public static PassportElementErrorDataField WithPassport(
            string fieldName,
            string dataHash,
            string message)
            =>
                new PassportElementErrorDataField(
                    PassportEnums.Scope.Passport,
                    fieldName,
                    dataHash,
                    message
                );
    }
}
