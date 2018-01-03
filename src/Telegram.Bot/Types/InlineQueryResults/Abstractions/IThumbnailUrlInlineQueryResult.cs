namespace Telegram.Bot.Types.InlineQueryResults.Abstractions
{
    public interface IThumbnailUrlInlineQueryResult
    {
        /// <summary>
        /// URL of the static thumbnail for the result.
        /// </summary>
        string ThumbUrl { get; set; }
    }
}