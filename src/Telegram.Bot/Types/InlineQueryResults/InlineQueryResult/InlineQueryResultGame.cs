

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents a <see cref="Game"/>.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class InlineQueryResultGame : InlineQueryResult
{
    /// <summary>
    /// Type of the result, must be game
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public override InlineQueryResultType Type => InlineQueryResultType.Game;

    /// <summary>
    /// Short name of the game
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string GameShortName { get; }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    /// <param name="id">Unique identifier of this result</param>
    /// <param name="gameShortName">Short name of the game</param>
    public InlineQueryResultGame(string id, string gameShortName)
        : base(id)
    {
        GameShortName = gameShortName;
    }
}
