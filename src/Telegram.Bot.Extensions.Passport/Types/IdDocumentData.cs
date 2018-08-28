using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// This object represents the data of an identity document.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class IdDocumentData : IDecryptedValue
    {
        /// <summary>
        /// Document number
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string DocumentNo;

        /// <summary>
        /// Optional. Date of expiry, in DD.MM.YYYY format
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ExpiryDate;

        public DateTime? Expiry
        {
            get
            {
                if (
                    !string.IsNullOrWhiteSpace(ExpiryDate) &&
                    DateTime.TryParseExact(ExpiryDate, "dd.MM.yyyy", null, DateTimeStyles.None, out var result)
                )
                {
                    return result;
                }

                return null;
            }
        }
    }
}
