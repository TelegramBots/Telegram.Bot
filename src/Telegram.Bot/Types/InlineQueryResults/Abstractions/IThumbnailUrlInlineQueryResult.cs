using System;

namespace Telegram.Bot.Types.InlineQueryResults.Abstractions
{
    public interface IThumbnailUrlInlineQueryResult
    {
        /// <summary>
        /// Optional. Url of the thumbnail for the result.
        /// </summary>
        Uri ThumbUrl { get; set; }
    }
}
