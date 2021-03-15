using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a dice with random value
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Dice
    {
        /// <summary>
        /// Emoji on which the dice throw animation is based
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Emoji { get; set; }
        /// <summary>
        /// Value of the dice, 1-6 for <see cref="Telegram.Bot.Types.Enums.Emoji.Dice" /> (“🎲”), <see cref="Telegram.Bot.Types.Enums.Emoji.Darts" /> (“🎯”) and <see cref="Telegram.Bot.Types.Enums.Emoji.Bowling"/> ("🎳"), 1-5 for <see cref="Telegram.Bot.Types.Enums.Emoji.Basketball" /> (“🏀”) and <see cref="Telegram.Bot.Types.Enums.Emoji.Football" />("⚽"), and values 1-64 for <see cref="Telegram.Bot.Types.Enums.Emoji.SlotMachine" /> ("🎰"). Defaults to <see cref="Telegram.Bot.Types.Enums.Emoji.Dice" /> (“🎲”)
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int Value { get; set; }
    }
}
