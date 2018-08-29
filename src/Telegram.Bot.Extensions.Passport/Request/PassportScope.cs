using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Passport.Request
{
    /// <summary>
    /// This object represents the data to be requested.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PassportScope
    {
        /// <summary>
        /// List of requested elements, each type may be used only once in the entire array of
        /// <see cref="IPassportScopeElement"/>  PassportScopeElement objects
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public IEnumerable<IPassportScopeElement> Data { get; set; }

        /// <summary>
        /// Scope version
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int V { get; set; } = 1;
    }
}
