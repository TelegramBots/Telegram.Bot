using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents one row of the high scores table for a game.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class GameHighScore
    {
        /// <summary>
        /// Position in high score table for the game.
        /// </summary>
        [JsonProperty("position", Required = Required.Always)]
        public int Position { get; set; }

        /// <summary>
        /// User.
        /// </summary>
        [JsonProperty("user", Required = Required.Always)]
        public User User { get; set; }

        /// <summary>
        /// Score.
        /// </summary>
        [JsonProperty("score", Required = Required.Always)]
        public int Score { get; set; }
    }
}
