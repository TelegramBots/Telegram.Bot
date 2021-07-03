namespace Telegram.Bot.Types.InlineQueryResults.Abstractions
{
    /// <summary>
    /// Represents an inline query result with location
    /// </summary>
    public interface ILocationInlineQueryResult
    {
        /// <summary>
        /// Location latitude in degrees
        /// </summary>
        float Latitude { get; }

        /// <summary>
        /// Location longitude in degrees
        /// </summary>
        float Longitude { get; }
    }
}
