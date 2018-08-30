using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Passport;

// ReSharper disable CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Informs a user that some of the Telegram Passport elements they provided contains errors.
    /// The user will not be able to re-submit their Passport to you until the errors are fixed (the contents of
    /// the field for which you returned the error must change). Returns True on success.
    /// Use this if the data submitted by the user doesn't satisfy the standards your service requires for any reason.
    /// For example, if a birthday date seems invalid, a submitted document is blurry, a scan shows evidence of
    /// tampering, etc. Supply some details in the error message to make sure the user knows how to correct the issues.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SetPassportDataErrorsRequest : RequestBase<bool>
    {
        /// <summary>
        /// User identifier
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int UserId { get; }

        /// <summary>
        /// Descriptions of the errors
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public IEnumerable<PassportElementError> Errors { get; }

        /// <summary>
        /// Initializes a new request with required parameters
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="errors">Descriptions of the errors</param>
        public SetPassportDataErrorsRequest(int userId, IEnumerable<PassportElementError> errors)
            : base("setPassportDataErrors")
        {
            UserId = userId;
            Errors = errors;
        }
    }
}
