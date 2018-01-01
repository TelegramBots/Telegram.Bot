using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a <see cref="Game"/>.
    /// </summary>
    /// <seealso cref="InlineQueryResult" />
    [JsonObject(MemberSerialization = MemberSerialization.OptIn,
                NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultGame : InlineQueryResult
    {
        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        public InlineQueryResultGame(string id)
            : base(id, InlineQueryResultType.Game)
        { }

        /// <summary>
        /// Short name of the game.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string GameShortName { get; set; }
    }
}
