using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Passport.Request
{
    /// <summary>
    /// This object represents one particular element that must be provided. If no options are needed, String
    /// can be used instead of this object to specify the type of the element.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PassportScopeElementOne : IPassportScopeElement
    {
        /// <summary>
        /// Element type. One of <see cref="PassportEnums.Scope"/>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Type { get; set; }

        /// <summary>
        /// Optional. Use this parameter if you want to request a selfie with the document as well.
        /// Available for "passport", "driver_license", "identity_card" and "internal_passport"
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Selfie { get; set; }

        /// <summary>
        /// Optional. Use this parameter if you want to request a translation of the document as well.
        /// Available for "passport", "driver_license", "identity_card", "internal_passport", "utility_bill",
        /// "bank_statement", "rental_agreement", "passport_registration" and "temporary_registration".
        /// Note: We suggest to only request translations after you have received a valid document that requires one.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Translation { get; set; }

        /// <summary>
        /// Optional. Use this parameter to request the first, last and middle name of the user in the language
        /// of the user's country of residence. Available for "personal_details"
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? NativeNames { get; set; }

        public PassportScopeElementOne(string type)
        {
            Type = type;
        }
    }
}
