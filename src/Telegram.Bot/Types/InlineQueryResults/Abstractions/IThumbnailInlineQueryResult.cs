namespace Telegram.Bot.Types.InlineQueryResults.Abstractions
{
    public interface IThumbnailInlineQueryResult : IThumbnailUrlInlineQueryResult
    {
        /// <summary>
        /// Optional. Thumbnail width.
        /// </summary>
        int ThumbWidth { get; set; }

        /// <summary>
        /// Optional. Thumbnail height.
        /// </summary>
        int ThumbHeight { get; set; }
    }
}
