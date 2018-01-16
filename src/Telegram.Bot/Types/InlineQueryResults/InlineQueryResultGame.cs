using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a <see cref="Game"/>.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultGame : InlineQueryResultBase
    {
        /// <summary>
        /// Short name of the game
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string GameShortName { get; set; }

        private InlineQueryResultGame()
            : base(InlineQueryResultType.Game)
        {
        }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="gameShortName">Short name of the game</param>
        public InlineQueryResultGame(string id, string gameShortName)
            : base(InlineQueryResultType.Game, id)
        {
            GameShortName = gameShortName;
        }
    }
}