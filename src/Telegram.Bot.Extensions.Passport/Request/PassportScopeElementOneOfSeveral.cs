using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Passport.Request
{
    /// <summary>
    /// This object represents several elements one of which must be provided.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PassportScopeElementOneOfSeveral : IPassportScopeElement
    {
        /// <summary>
        /// List of elements one of which must be provided; must contain either several of "passport",
        /// "driver_license", "identity_card", "internal_passport" or several of "utility_bill", "bank_statement",
        /// "rental_agreement", "passport_registration", "temporary_registration"
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public IEnumerable<PassportScopeElementOne> OneOf { get; }

        /// <summary>
        /// Optional. Use this parameter if you want to request a selfie with the document from this list that
        /// the user chooses to upload.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Selfie { get; set; }

        /// <summary>
        /// Optional. Use this parameter if you want to request a translation of the document from this list
        /// that the user chooses to upload.
        /// Note: We suggest to only request translations after you have received a valid document that requires one.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Translation { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="PassportScopeElementOneOfSeveral"/> with required parameter
        /// </summary>
        /// <param name="oneOf">
        /// List of elements one of which must be provided; must contain either several of "passport",
        /// "driver_license", "identity_card", "internal_passport" or several of "utility_bill", "bank_statement",
        /// "rental_agreement", "passport_registration", "temporary_registration"
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        public PassportScopeElementOneOfSeveral(IEnumerable<PassportScopeElementOne> oneOf)
        {
            OneOf = oneOf ?? throw new ArgumentNullException(nameof(oneOf));
        }
    }
}
