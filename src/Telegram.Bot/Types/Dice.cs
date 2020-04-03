using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a dice with random value from 1 to 6
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Dice
    {
        /// <summary>
        /// Value of the dice, 1-6
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int Value { get; set; }
    }
}
