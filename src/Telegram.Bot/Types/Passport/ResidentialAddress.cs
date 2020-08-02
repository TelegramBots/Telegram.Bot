using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// This object represents a residential address.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ResidentialAddress : IDecryptedValue
    {
        /// <summary>
        /// First line for the address
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string StreetLine1 { get; set; } = default!;

        /// <summary>
        /// Optional. Second line for the address
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? StreetLine2 { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string City { get; set; } = default!;

        /// <summary>
        /// Optional. State
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? State { get; set; }

        /// <summary>
        /// ISO 3166-1 alpha-2 country code
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string CountryCode { get; set; } = default!;

        /// <summary>
        /// Address post code
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string PostCode { get; set; } = default!;
    }
}
